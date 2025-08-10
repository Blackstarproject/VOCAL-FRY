// CREATED BY: JUSTIN LINWOOD ROSS || 8-10-2025 || https://github.com/Blackstarproject?tab=repositories
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;// Required for JSON serialization for the preset system, GO TO ASSEMBLIES AND ADD IT IF NOT ALREADY THERE
using System.Windows.Forms;


namespace Vocal_Fry
{
    public partial class Form1 : Form
    {
        // Core NAudio components
        private IWaveIn waveIn;
        private IWavePlayer waveOut;
        private BufferedWaveProvider bufferedWaveProvider;

        // Effect providers
        private PitchShiftingSampleProvider pitchShiftingProvider;
        private DistortionSampleProvider distortionProvider;
        private ReverbSampleProvider reverbProvider;
        private FlangerSampleProvider flangerProvider; 

        // UI and Feature Components
        private bool isRecording = false;
        private readonly Dictionary<string, EffectsPreset> presets = new Dictionary<string, EffectsPreset>();
        private readonly string presetsFilePath;


        public Form1()
        {
            // The InitializeComponent() call loads the UI from the designer file
            InitializeComponent();
            presetsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VocalFry_Advanced", "presets.json");

            InitializeAudio();
            LoadPresets();
            UpdatePresetComboBox();
        }

        private void InitializeAudio()
        {
            try
            {
                var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 1);

                waveIn = new WaveInEvent
                {
                    WaveFormat = waveFormat,
                    DeviceNumber = 0,
                    BufferMilliseconds = 20
                };
                waveIn.DataAvailable += OnDataAvailable;

                bufferedWaveProvider = new BufferedWaveProvider(waveFormat)
                {
                    BufferDuration = TimeSpan.FromMinutes(5),
                    DiscardOnBufferOverflow = true
                };

                waveOut = new WaveOutEvent();

                // *** Create the advanced effect chain ***
                ISampleProvider currentProvider = bufferedWaveProvider.ToSampleProvider();
                currentProvider = pitchShiftingProvider = new PitchShiftingSampleProvider(currentProvider);
                currentProvider = distortionProvider = new DistortionSampleProvider(currentProvider);
                currentProvider = reverbProvider = new ReverbSampleProvider(currentProvider);
                currentProvider = flangerProvider = new FlangerSampleProvider(currentProvider);

                waveOut.Init(currentProvider);

                // Set initial UI values from default effect parameters
                InitializeControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A critical error occurred during audio initialization: {ex.Message}", "Audio Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStartStop.Enabled = false;
            }
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            bufferedWaveProvider?.AddSamples(e.Buffer, 0, e.BytesRecorded);
            // Update the waveform visualizer with the new raw audio data
            DrawWaveform(e.Buffer, e.BytesRecorded);
        }

        #region UI Event Handlers

        private void BtnStartStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isRecording)
                {
                    waveIn.StartRecording();
                    waveOut.Play();
                    btnStartStop.Text = "Stop";
                    isRecording = true;
                }
                else
                {
                    waveIn.StopRecording();
                    waveOut.Stop();
                    btnStartStop.Text = "Start";
                    isRecording = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting/stopping audio: {ex.Message}", "Runtime Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Effect Control Handlers ---
        private void TrackBarPitch_Scroll(object sender, EventArgs e)
        {
            pitchShiftingProvider.PitchRatio = trackBarPitch.Value / 10.0f;
            lblPitchValue.Text = pitchShiftingProvider.PitchRatio.ToString("F2");
        }

        private void TrackBarDistortion_Scroll(object sender, EventArgs e)
        {
            distortionProvider.Gain = trackBarDistortion.Value;
            lblDistortionValue.Text = distortionProvider.Gain.ToString("F1");
        }

        private void TrackBarReverb_Scroll(object sender, EventArgs e)
        {
            reverbProvider.Feedback = trackBarReverb.Value / 100.0f;
            lblReverbValue.Text = reverbProvider.Feedback.ToString("F2");
        }

        private void TrackBarFlangerRate_Scroll(object sender, EventArgs e)
        {
            flangerProvider.Rate = trackBarFlangerRate.Value / 10.0f;
            lblFlangerRateValue.Text = flangerProvider.Rate.ToString("F1") + " Hz";
        }

        private void TrackBarFlangerDepth_Scroll(object sender, EventArgs e)
        {
            flangerProvider.Depth = trackBarFlangerDepth.Value / 10.0f;
            lblFlangerDepthValue.Text = flangerProvider.Depth.ToString("F1") + " ms";
        }

        private void TrackBarFlangerFeedback_Scroll(object sender, EventArgs e)
        {
            flangerProvider.Feedback = trackBarFlangerFeedback.Value / 100.0f;
            lblFlangerFeedbackValue.Text = flangerProvider.Feedback.ToString("F2");
        }

        private void TrackBarFlangerMix_Scroll(object sender, EventArgs e)
        {
            flangerProvider.Mix = trackBarFlangerMix.Value / 100.0f;
            lblFlangerMixValue.Text = flangerProvider.Mix.ToString("F2");
        }

        #endregion

        #region Waveform Visualization

        private void DrawWaveform(byte[] buffer, int bytesRecorded)
        {
            if (waveformVisualizer.Image == null)
            {
                waveformVisualizer.Image = new Bitmap(waveformVisualizer.Width, waveformVisualizer.Height);
            }

            using (Graphics g = Graphics.FromImage(waveformVisualizer.Image))
            {
                g.Clear(Color.Black);
                Pen whitePen = new Pen(Color.White, 1);
                int centerY = waveformVisualizer.Height / 2;
                int samples = bytesRecorded / 4;
                if (samples == 0) return;

                float maxVal = 0;
                for (int i = 0; i < samples; i++)
                {
                    float sampleValue = BitConverter.ToSingle(buffer, i * 4);
                    if (Math.Abs(sampleValue) > maxVal) maxVal = Math.Abs(sampleValue);
                }
                if (maxVal == 0) maxVal = 1;

                PointF[] points = new PointF[waveformVisualizer.Width];
                for (int i = 0; i < waveformVisualizer.Width; i++)
                {
                    int sampleIndex = (int)((float)i / waveformVisualizer.Width * (samples - 1));
                    float sampleValue = BitConverter.ToSingle(buffer, sampleIndex * 4);
                    float y = centerY - (sampleValue / maxVal * centerY);
                    points[i] = new PointF(i, y);
                }
                g.DrawLines(whitePen, points);
            }
            waveformVisualizer.Invalidate();
        }

        #endregion

        #region Preset System

        private void BtnSavePreset_Click(object sender, EventArgs e)
        {
            string name = Prompt.ShowDialog("Enter Preset Name", "Save Preset");
            if (!string.IsNullOrWhiteSpace(name) && !presets.ContainsKey(name))
            {
                var preset = new EffectsPreset
                {
                    Pitch = pitchShiftingProvider.PitchRatio,
                    Distortion = distortionProvider.Gain,
                    Reverb = reverbProvider.Feedback,
                    FlangerRate = flangerProvider.Rate,
                    FlangerDepth = flangerProvider.Depth,
                    FlangerFeedback = flangerProvider.Feedback,
                    FlangerMix = flangerProvider.Mix,
                };
                presets[name] = preset;
                SavePresets();
                UpdatePresetComboBox();
                comboPresets.SelectedItem = name;
            }
            else if (presets.ContainsKey(name))
            {
                MessageBox.Show("A preset with this name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeletePreset_Click(object sender, EventArgs e)
        {
            if (comboPresets.SelectedItem != null)
            {
                string name = comboPresets.SelectedItem.ToString();
                if (MessageBox.Show($"Are you sure you want to delete the preset '{name}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    presets.Remove(name);
                    SavePresets();
                    UpdatePresetComboBox();
                }
            }
        }

        private void ComboPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPresets.SelectedItem != null)
            {
                string name = comboPresets.SelectedItem.ToString();
                if (presets.ContainsKey(name))
                {
                    ApplyPreset(presets[name]);
                }
            }
        }

        private void ApplyPreset(EffectsPreset preset)
        {
            pitchShiftingProvider.PitchRatio = preset.Pitch;
            distortionProvider.Gain = preset.Distortion;
            reverbProvider.Feedback = preset.Reverb;
            flangerProvider.Rate = preset.FlangerRate;
            flangerProvider.Depth = preset.FlangerDepth;
            flangerProvider.Feedback = preset.FlangerFeedback;
            flangerProvider.Mix = preset.FlangerMix;

            InitializeControls();
        }

        private void InitializeControls()
        {
            trackBarPitch.Value = (int)(pitchShiftingProvider.PitchRatio * 10);
            trackBarDistortion.Value = (int)distortionProvider.Gain;
            trackBarReverb.Value = (int)(reverbProvider.Feedback * 100);
            trackBarFlangerRate.Value = (int)(flangerProvider.Rate * 10);
            trackBarFlangerDepth.Value = (int)(flangerProvider.Depth * 10);
            trackBarFlangerFeedback.Value = (int)(flangerProvider.Feedback * 100);
            trackBarFlangerMix.Value = (int)(flangerProvider.Mix * 100);

            // Trigger scroll events to update labels
            TrackBarPitch_Scroll(null, null);
            TrackBarDistortion_Scroll(null, null);
            TrackBarReverb_Scroll(null, null);
            TrackBarFlangerRate_Scroll(null, null);
            TrackBarFlangerDepth_Scroll(null, null);
            TrackBarFlangerFeedback_Scroll(null, null);
            TrackBarFlangerMix_Scroll(null, null);
        }

        private void UpdatePresetComboBox()
        {
            object selectedItem = comboPresets.SelectedItem;
            comboPresets.Items.Clear();
            foreach (var name in presets.Keys.OrderBy(k => k))
            {
                comboPresets.Items.Add(name);
            }
            if (selectedItem != null && comboPresets.Items.Contains(selectedItem))
            {
                comboPresets.SelectedItem = selectedItem;
            }
            else if (comboPresets.Items.Count > 0)
            {
                comboPresets.SelectedIndex = 0;
            }
        }

        private void SavePresets()
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(presets);
                Directory.CreateDirectory(Path.GetDirectoryName(presetsFilePath));
                File.WriteAllText(presetsFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving presets: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPresets()
        {
            if (!File.Exists(presetsFilePath)) return;
            try
            {
                var serializer = new JavaScriptSerializer();
                string json = File.ReadAllText(presetsFilePath);
                var loadedPresets = serializer.Deserialize<Dictionary<string, EffectsPreset>>(json);
                if (loadedPresets != null)
                {
                    foreach (var p in loadedPresets)
                    {
                        presets[p.Key] = p.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                presets.Clear();
                MessageBox.Show($"Error loading presets file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class EffectsPreset
        {
            public float Pitch { get; set; }
            public float Distortion { get; set; }
            public float Reverb { get; set; }
            public float FlangerRate { get; set; }
            public float FlangerDepth { get; set; }
            public float FlangerFeedback { get; set; }
            public float FlangerMix { get; set; }
        }

        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 400,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text, AutoSize = true };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 300 };
                Button confirmation = new Button() { Text = "Ok", Left = 250, Width = 100, Top = 80, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            waveOut?.Stop();
            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
            }
            waveOut?.Dispose();
            waveOut = null;
            base.OnFormClosing(e);
        }
    }
}
