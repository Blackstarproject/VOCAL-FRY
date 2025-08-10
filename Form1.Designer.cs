namespace Vocal_Fry
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnStartStop = new System.Windows.Forms.Button();
            this.trackBarPitch = new System.Windows.Forms.TrackBar();
            this.lblPitchValue = new System.Windows.Forms.Label();
            this.trackBarDistortion = new System.Windows.Forms.TrackBar();
            this.lblDistortionValue = new System.Windows.Forms.Label();
            this.trackBarReverb = new System.Windows.Forms.TrackBar();
            this.lblReverbValue = new System.Windows.Forms.Label();
            this.waveformVisualizer = new System.Windows.Forms.PictureBox();
            this.groupBoxEffects = new System.Windows.Forms.GroupBox();
            this.lblFlangerMixValue = new System.Windows.Forms.Label();
            this.trackBarFlangerMix = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFlangerFeedbackValue = new System.Windows.Forms.Label();
            this.trackBarFlangerFeedback = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFlangerDepthValue = new System.Windows.Forms.Label();
            this.trackBarFlangerDepth = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFlangerRateValue = new System.Windows.Forms.Label();
            this.trackBarFlangerRate = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxPresets = new System.Windows.Forms.GroupBox();
            this.btnDeletePreset = new System.Windows.Forms.Button();
            this.btnSavePreset = new System.Windows.Forms.Button();
            this.comboPresets = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDistortion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReverb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waveformVisualizer)).BeginInit();
            this.groupBoxEffects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFlangerMix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFlangerFeedback)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFlangerDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFlangerRate)).BeginInit();
            this.groupBoxPresets.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartStop
            // 
            this.btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartStop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnStartStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnStartStop.Location = new System.Drawing.Point(627, 482);
            this.btnStartStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(183, 59);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.BtnStartStop_Click);
            // 
            // trackBarPitch
            // 
            this.trackBarPitch.Location = new System.Drawing.Point(16, 59);
            this.trackBarPitch.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarPitch.Maximum = 20;
            this.trackBarPitch.Minimum = 1;
            this.trackBarPitch.Name = "trackBarPitch";
            this.trackBarPitch.Size = new System.Drawing.Size(344, 56);
            this.trackBarPitch.TabIndex = 1;
            this.trackBarPitch.TickFrequency = 2;
            this.trackBarPitch.Value = 10;
            this.trackBarPitch.Scroll += new System.EventHandler(this.TrackBarPitch_Scroll);
            // 
            // lblPitchValue
            // 
            this.lblPitchValue.AutoSize = true;
            this.lblPitchValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPitchValue.ForeColor = System.Drawing.Color.White;
            this.lblPitchValue.Location = new System.Drawing.Point(139, 34);
            this.lblPitchValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPitchValue.Name = "lblPitchValue";
            this.lblPitchValue.Size = new System.Drawing.Size(40, 18);
            this.lblPitchValue.TabIndex = 2;
            this.lblPitchValue.Text = "1.00";
            // 
            // trackBarDistortion
            // 
            this.trackBarDistortion.Location = new System.Drawing.Point(16, 142);
            this.trackBarDistortion.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarDistortion.Maximum = 100;
            this.trackBarDistortion.Name = "trackBarDistortion";
            this.trackBarDistortion.Size = new System.Drawing.Size(344, 56);
            this.trackBarDistortion.TabIndex = 3;
            this.trackBarDistortion.TickFrequency = 10;
            this.trackBarDistortion.Scroll += new System.EventHandler(this.TrackBarDistortion_Scroll);
            // 
            // lblDistortionValue
            // 
            this.lblDistortionValue.AutoSize = true;
            this.lblDistortionValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistortionValue.ForeColor = System.Drawing.Color.White;
            this.lblDistortionValue.Location = new System.Drawing.Point(139, 117);
            this.lblDistortionValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDistortionValue.Name = "lblDistortionValue";
            this.lblDistortionValue.Size = new System.Drawing.Size(31, 18);
            this.lblDistortionValue.TabIndex = 4;
            this.lblDistortionValue.Text = "0.0";
            // 
            // trackBarReverb
            // 
            this.trackBarReverb.Location = new System.Drawing.Point(16, 225);
            this.trackBarReverb.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarReverb.Maximum = 95;
            this.trackBarReverb.Name = "trackBarReverb";
            this.trackBarReverb.Size = new System.Drawing.Size(344, 56);
            this.trackBarReverb.TabIndex = 5;
            this.trackBarReverb.TickFrequency = 10;
            this.trackBarReverb.Scroll += new System.EventHandler(this.TrackBarReverb_Scroll);
            // 
            // lblReverbValue
            // 
            this.lblReverbValue.AutoSize = true;
            this.lblReverbValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReverbValue.ForeColor = System.Drawing.Color.White;
            this.lblReverbValue.Location = new System.Drawing.Point(139, 201);
            this.lblReverbValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReverbValue.Name = "lblReverbValue";
            this.lblReverbValue.Size = new System.Drawing.Size(40, 18);
            this.lblReverbValue.TabIndex = 6;
            this.lblReverbValue.Text = "0.00";
            // 
            // waveformVisualizer
            // 
            this.waveformVisualizer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.waveformVisualizer.BackColor = System.Drawing.Color.Black;
            this.waveformVisualizer.Location = new System.Drawing.Point(12, 559);
            this.waveformVisualizer.Name = "waveformVisualizer";
            this.waveformVisualizer.Size = new System.Drawing.Size(798, 118);
            this.waveformVisualizer.TabIndex = 7;
            this.waveformVisualizer.TabStop = false;
            // 
            // groupBoxEffects
            // 
            this.groupBoxEffects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxEffects.Controls.Add(this.lblFlangerMixValue);
            this.groupBoxEffects.Controls.Add(this.trackBarFlangerMix);
            this.groupBoxEffects.Controls.Add(this.label5);
            this.groupBoxEffects.Controls.Add(this.lblFlangerFeedbackValue);
            this.groupBoxEffects.Controls.Add(this.trackBarFlangerFeedback);
            this.groupBoxEffects.Controls.Add(this.label4);
            this.groupBoxEffects.Controls.Add(this.lblFlangerDepthValue);
            this.groupBoxEffects.Controls.Add(this.trackBarFlangerDepth);
            this.groupBoxEffects.Controls.Add(this.label3);
            this.groupBoxEffects.Controls.Add(this.lblFlangerRateValue);
            this.groupBoxEffects.Controls.Add(this.trackBarFlangerRate);
            this.groupBoxEffects.Controls.Add(this.label2);
            this.groupBoxEffects.Controls.Add(this.label1);
            this.groupBoxEffects.Controls.Add(this.lblReverbValue);
            this.groupBoxEffects.Controls.Add(this.trackBarPitch);
            this.groupBoxEffects.Controls.Add(this.trackBarReverb);
            this.groupBoxEffects.Controls.Add(this.lblPitchValue);
            this.groupBoxEffects.Controls.Add(this.lblDistortionValue);
            this.groupBoxEffects.Controls.Add(this.trackBarDistortion);
            this.groupBoxEffects.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxEffects.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxEffects.Location = new System.Drawing.Point(12, 12);
            this.groupBoxEffects.Name = "groupBoxEffects";
            this.groupBoxEffects.Size = new System.Drawing.Size(798, 321);
            this.groupBoxEffects.TabIndex = 8;
            this.groupBoxEffects.TabStop = false;
            this.groupBoxEffects.Text = "Effects";
            // 
            // lblFlangerMixValue
            // 
            this.lblFlangerMixValue.AutoSize = true;
            this.lblFlangerMixValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFlangerMixValue.ForeColor = System.Drawing.Color.White;
            this.lblFlangerMixValue.Location = new System.Drawing.Point(553, 284);
            this.lblFlangerMixValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFlangerMixValue.Name = "lblFlangerMixValue";
            this.lblFlangerMixValue.Size = new System.Drawing.Size(40, 18);
            this.lblFlangerMixValue.TabIndex = 19;
            this.lblFlangerMixValue.Text = "0.50";
            // 
            // trackBarFlangerMix
            // 
            this.trackBarFlangerMix.Location = new System.Drawing.Point(430, 225);
            this.trackBarFlangerMix.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarFlangerMix.Maximum = 100;
            this.trackBarFlangerMix.Name = "trackBarFlangerMix";
            this.trackBarFlangerMix.Size = new System.Drawing.Size(344, 56);
            this.trackBarFlangerMix.TabIndex = 18;
            this.trackBarFlangerMix.TickFrequency = 10;
            this.trackBarFlangerMix.Value = 50;
            this.trackBarFlangerMix.Scroll += new System.EventHandler(this.TrackBarFlangerMix_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(426, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 23);
            this.label5.TabIndex = 17;
            this.label5.Text = "Flanger Mix";
            // 
            // lblFlangerFeedbackValue
            // 
            this.lblFlangerFeedbackValue.AutoSize = true;
            this.lblFlangerFeedbackValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFlangerFeedbackValue.ForeColor = System.Drawing.Color.White;
            this.lblFlangerFeedbackValue.Location = new System.Drawing.Point(553, 117);
            this.lblFlangerFeedbackValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFlangerFeedbackValue.Name = "lblFlangerFeedbackValue";
            this.lblFlangerFeedbackValue.Size = new System.Drawing.Size(40, 18);
            this.lblFlangerFeedbackValue.TabIndex = 16;
            this.lblFlangerFeedbackValue.Text = "0.60";
            // 
            // trackBarFlangerFeedback
            // 
            this.trackBarFlangerFeedback.Location = new System.Drawing.Point(430, 142);
            this.trackBarFlangerFeedback.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarFlangerFeedback.Maximum = 95;
            this.trackBarFlangerFeedback.Name = "trackBarFlangerFeedback";
            this.trackBarFlangerFeedback.Size = new System.Drawing.Size(344, 56);
            this.trackBarFlangerFeedback.TabIndex = 15;
            this.trackBarFlangerFeedback.TickFrequency = 10;
            this.trackBarFlangerFeedback.Value = 60;
            this.trackBarFlangerFeedback.Scroll += new System.EventHandler(this.TrackBarFlangerFeedback_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(426, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 23);
            this.label4.TabIndex = 14;
            this.label4.Text = "Flanger Feedback";
            // 
            // lblFlangerDepthValue
            // 
            this.lblFlangerDepthValue.AutoSize = true;
            this.lblFlangerDepthValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFlangerDepthValue.ForeColor = System.Drawing.Color.White;
            this.lblFlangerDepthValue.Location = new System.Drawing.Point(553, 34);
            this.lblFlangerDepthValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFlangerDepthValue.Name = "lblFlangerDepthValue";
            this.lblFlangerDepthValue.Size = new System.Drawing.Size(59, 18);
            this.lblFlangerDepthValue.TabIndex = 13;
            this.lblFlangerDepthValue.Text = "5.0 ms";
            // 
            // trackBarFlangerDepth
            // 
            this.trackBarFlangerDepth.Location = new System.Drawing.Point(430, 59);
            this.trackBarFlangerDepth.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarFlangerDepth.Maximum = 150;
            this.trackBarFlangerDepth.Minimum = 1;
            this.trackBarFlangerDepth.Name = "trackBarFlangerDepth";
            this.trackBarFlangerDepth.Size = new System.Drawing.Size(344, 56);
            this.trackBarFlangerDepth.TabIndex = 12;
            this.trackBarFlangerDepth.TickFrequency = 10;
            this.trackBarFlangerDepth.Value = 50;
            this.trackBarFlangerDepth.Scroll += new System.EventHandler(this.TrackBarFlangerDepth_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(426, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 23);
            this.label3.TabIndex = 11;
            this.label3.Text = "Flanger Depth";
            // 
            // lblFlangerRateValue
            // 
            this.lblFlangerRateValue.AutoSize = true;
            this.lblFlangerRateValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFlangerRateValue.ForeColor = System.Drawing.Color.White;
            this.lblFlangerRateValue.Location = new System.Drawing.Point(139, 284);
            this.lblFlangerRateValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFlangerRateValue.Name = "lblFlangerRateValue";
            this.lblFlangerRateValue.Size = new System.Drawing.Size(57, 18);
            this.lblFlangerRateValue.TabIndex = 10;
            this.lblFlangerRateValue.Text = "0.5 Hz";
            // 
            // trackBarFlangerRate
            // 
            this.trackBarFlangerRate.Location = new System.Drawing.Point(16, 259);
            this.trackBarFlangerRate.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarFlangerRate.Maximum = 50;
            this.trackBarFlangerRate.Name = "trackBarFlangerRate";
            this.trackBarFlangerRate.Size = new System.Drawing.Size(344, 56);
            this.trackBarFlangerRate.TabIndex = 9;
            this.trackBarFlangerRate.Value = 5;
            this.trackBarFlangerRate.Scroll += new System.EventHandler(this.TrackBarFlangerRate_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "Distortion";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "Pitch Shift";
            // 
            // groupBoxPresets
            // 
            this.groupBoxPresets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPresets.Controls.Add(this.btnDeletePreset);
            this.groupBoxPresets.Controls.Add(this.btnSavePreset);
            this.groupBoxPresets.Controls.Add(this.comboPresets);
            this.groupBoxPresets.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.groupBoxPresets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxPresets.Location = new System.Drawing.Point(12, 353);
            this.groupBoxPresets.Name = "groupBoxPresets";
            this.groupBoxPresets.Size = new System.Drawing.Size(588, 188);
            this.groupBoxPresets.TabIndex = 9;
            this.groupBoxPresets.TabStop = false;
            this.groupBoxPresets.Text = "Presets";
            // 
            // btnDeletePreset
            // 
            this.btnDeletePreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePreset.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeletePreset.Location = new System.Drawing.Point(298, 102);
            this.btnDeletePreset.Name = "btnDeletePreset";
            this.btnDeletePreset.Size = new System.Drawing.Size(262, 49);
            this.btnDeletePreset.TabIndex = 2;
            this.btnDeletePreset.Text = "Delete Selected Preset";
            this.btnDeletePreset.UseVisualStyleBackColor = true;
            this.btnDeletePreset.Click += new System.EventHandler(this.BtnDeletePreset_Click);
            // 
            // btnSavePreset
            // 
            this.btnSavePreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePreset.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSavePreset.Location = new System.Drawing.Point(19, 102);
            this.btnSavePreset.Name = "btnSavePreset";
            this.btnSavePreset.Size = new System.Drawing.Size(262, 49);
            this.btnSavePreset.TabIndex = 1;
            this.btnSavePreset.Text = "Save Current as Preset";
            this.btnSavePreset.UseVisualStyleBackColor = true;
            this.btnSavePreset.Click += new System.EventHandler(this.BtnSavePreset_Click);
            // 
            // comboPresets
            // 
            this.comboPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPresets.FormattingEnabled = true;
            this.comboPresets.Location = new System.Drawing.Point(19, 47);
            this.comboPresets.Name = "comboPresets";
            this.comboPresets.Size = new System.Drawing.Size(541, 31);
            this.comboPresets.TabIndex = 0;
            this.comboPresets.SelectedIndexChanged += new System.EventHandler(this.ComboPresets_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(822, 689);
            this.Controls.Add(this.groupBoxPresets);
            this.Controls.Add(this.groupBoxEffects);
            this.Controls.Add(this.waveformVisualizer);
            this.Controls.Add(this.btnStartStop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(840, 736);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VOCAL FRY";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDistortion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReverb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waveformVisualizer)).EndInit();
            this.groupBoxEffects.ResumeLayout(false);
            this.groupBoxEffects.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFlangerMix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFlangerFeedback)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFlangerDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFlangerRate)).EndInit();
            this.groupBoxPresets.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.TrackBar trackBarPitch;
        private System.Windows.Forms.Label lblPitchValue;
        private System.Windows.Forms.TrackBar trackBarDistortion;
        private System.Windows.Forms.Label lblDistortionValue;
        private System.Windows.Forms.TrackBar trackBarReverb;
        private System.Windows.Forms.Label lblReverbValue;
        private System.Windows.Forms.PictureBox waveformVisualizer;
        private System.Windows.Forms.GroupBox groupBoxEffects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFlangerRateValue;
        private System.Windows.Forms.TrackBar trackBarFlangerRate;
        private System.Windows.Forms.Label lblFlangerDepthValue;
        private System.Windows.Forms.TrackBar trackBarFlangerDepth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFlangerFeedbackValue;
        private System.Windows.Forms.TrackBar trackBarFlangerFeedback;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFlangerMixValue;
        private System.Windows.Forms.TrackBar trackBarFlangerMix;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBoxPresets;
        private System.Windows.Forms.Button btnDeletePreset;
        private System.Windows.Forms.Button btnSavePreset;
        private System.Windows.Forms.ComboBox comboPresets;
    }
}
