// Decompiled with JetBrains decompiler
// Type: FreeISOCreator.MainForm
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using Export;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FreeISOCreator
{
  public class MainForm : Form
  {
    private Thread m_thread;
    private IsoCreator.IsoCreator m_creator;
    private IContainer components;
    private Label label1;
    private TextBox textBoxSourceFolder;
    private Button buttonBrowse;
    private Label label2;
    private TextBox textBoxISOFile;
    private Button buttonSaveas;
    private TextBox textBoxVolumeName;
    private Label label3;
    private Button buttonCreate;
    private GroupBox groupBox;
    private ProgressBar progressBar;
    private LinkLabel linkLabel;
    private Label labelStatus;
    private Button buttonClose;

    public MainForm()
    {
      this.InitializeComponent();
      this.textBoxSourceFolder.Text = "";
      this.textBoxISOFile.Text = "";
      this.textBoxVolumeName.Text = "";
      this.m_creator = new IsoCreator.IsoCreator();
      this.m_creator.Progress += new ProgressDelegate(this.creator_Progress);
      this.m_creator.Finish += new FinishDelegate(this.creator_Finished);
      this.m_creator.Abort += new AbortDelegate(this.creator_Abort);
    }

    private void SetLabelStatus(string text)
    {
      this.labelStatus.Text = text;
      this.labelStatus.Refresh();
    }

    private void SetProgressValue(int value) => this.progressBar.Value = value;

    private void SetProgressMaximum(int maximum) => this.progressBar.Maximum = maximum;

    private void buttonCreate_Click(object sender, EventArgs e)
    {
      if (this.m_thread == null || !this.m_thread.IsAlive)
      {
        this.textBoxSourceFolder.Text = this.textBoxSourceFolder.Text.Trim();
        this.textBoxVolumeName.Text = this.textBoxVolumeName.Text.Trim();
        this.textBoxISOFile.Text = this.textBoxISOFile.Text.Trim();
        if (this.textBoxSourceFolder.Text == "")
        {
          int num1 = (int) MessageBox.Show("Please input a Source Folder", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else if (this.textBoxVolumeName.Text == "")
        {
          int num2 = (int) MessageBox.Show("Please input a Volume Name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else if (this.textBoxISOFile.Text == "")
        {
          int num3 = (int) MessageBox.Show("Please input a ISO File Name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else if (!Directory.Exists(this.textBoxSourceFolder.Text))
        {
          int num4 = (int) MessageBox.Show("The Source Folder \"" + this.textBoxSourceFolder.Text + "\" don't exists.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
        {
          if (File.Exists(this.textBoxISOFile.Text) && MessageBox.Show("The ISO File \"" + this.textBoxISOFile.Text + "\" already exists. Do you want to overwrite it?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;
          this.m_thread = new Thread(new ParameterizedThreadStart(this.m_creator.Folder2Iso));
          this.m_thread.Start((object) new IsoCreator.IsoCreator.IsoCreatorFolderArgs(this.textBoxSourceFolder.Text, this.textBoxISOFile.Text, this.textBoxVolumeName.Text));
          this.labelStatus.Text = "";
          this.buttonCreate.Text = "Abort";
        }
      }
      else
      {
        if (MessageBox.Show("Are you sure you want to abort the process?", "Abort", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return;
        this.m_thread.Abort();
        int num = (int) MessageBox.Show("The ISO creating process has been stopped.", "Abort", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.buttonCreate.Enabled = true;
        this.buttonCreate.Text = "Create";
        this.progressBar.Value = 0;
        this.progressBar.Maximum = 0;
        this.labelStatus.Text = "Process not started";
        this.Refresh();
      }
    }

    private void creator_Abort(object sender, AbortEventArgs e)
    {
    }

    private void creator_Finished(object sender, FinishEventArgs e)
    {
      int num = (int) MessageBox.Show(e.Message, "Finish", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      this.buttonCreate.Text = "Create";
      this.labelStatus.Text = "Finish";
      this.buttonCreate.Enabled = true;
      this.buttonCreate.Refresh();
      this.Refresh();
    }

    private void creator_Progress(object sender, ProgressEventArgs e)
    {
      if (e.Action != null)
      {
        if (!this.InvokeRequired)
          this.SetLabelStatus(e.Action);
        else
          this.Invoke((Delegate) new MainForm.SetLabelDelegate(this.SetLabelStatus), (object) e.Action);
      }
      if (e.Maximum != -1)
      {
        if (!this.InvokeRequired)
          this.SetProgressMaximum(e.Maximum);
        else
          this.Invoke((Delegate) new MainForm.SetNumericValueDelegate(this.SetProgressMaximum), (object) e.Maximum);
      }
      if (!this.InvokeRequired)
        this.progressBar.Value = e.Current <= this.progressBar.Maximum ? e.Current : this.progressBar.Maximum;
      else
        this.Invoke((Delegate) new MainForm.SetNumericValueDelegate(this.SetProgressValue), (object) (e.Current <= this.progressBar.Maximum ? e.Current : this.progressBar.Maximum));
    }

    private void buttonBrowse_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
      if (folderBrowserDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
        return;
      this.textBoxSourceFolder.Text = folderBrowserDialog.SelectedPath;
      this.textBoxVolumeName.Text = Path.GetFileName(folderBrowserDialog.SelectedPath);
    }

    private void buttonSaveas_Click(object sender, EventArgs e)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = "ISO Image Files (*.iso)|*.iso";
      if (saveFileDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
        return;
      this.textBoxISOFile.Text = saveFileDialog.FileName;
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.m_creator == null || this.m_thread == null || !this.m_thread.IsAlive)
        return;
      this.m_thread.Abort();
    }

    private void buttonClose_Click(object sender, EventArgs e) => this.Close();

    private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("IExplore", "www.freeisocreator.com");

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.textBoxSourceFolder = new TextBox();
      this.buttonBrowse = new Button();
      this.label2 = new Label();
      this.textBoxISOFile = new TextBox();
      this.buttonSaveas = new Button();
      this.textBoxVolumeName = new TextBox();
      this.label3 = new Label();
      this.buttonCreate = new Button();
      this.groupBox = new GroupBox();
      this.labelStatus = new Label();
      this.progressBar = new ProgressBar();
      this.linkLabel = new LinkLabel();
      this.buttonClose = new Button();
      this.groupBox.SuspendLayout();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(21, 24);
      this.label1.Name = "label1";
      this.label1.Size = new Size(89, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "Source Folder:";
      this.textBoxSourceFolder.Location = new Point(116, 21);
      this.textBoxSourceFolder.Name = "textBoxSourceFolder";
      this.textBoxSourceFolder.Size = new Size(220, 21);
      this.textBoxSourceFolder.TabIndex = 1;
      this.buttonBrowse.Location = new Point(347, 19);
      this.buttonBrowse.Name = "buttonBrowse";
      this.buttonBrowse.Size = new Size(75, 23);
      this.buttonBrowse.TabIndex = 2;
      this.buttonBrowse.Text = "Browse";
      this.buttonBrowse.UseVisualStyleBackColor = true;
      this.buttonBrowse.Click += new EventHandler(this.buttonBrowse_Click);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(21, 97);
      this.label2.Name = "label2";
      this.label2.Size = new Size(59, 12);
      this.label2.TabIndex = 3;
      this.label2.Text = "ISO File:";
      this.textBoxISOFile.Location = new Point(116, 94);
      this.textBoxISOFile.Name = "textBoxISOFile";
      this.textBoxISOFile.Size = new Size(220, 21);
      this.textBoxISOFile.TabIndex = 4;
      this.buttonSaveas.Location = new Point(347, 92);
      this.buttonSaveas.Name = "buttonSaveas";
      this.buttonSaveas.Size = new Size(75, 23);
      this.buttonSaveas.TabIndex = 5;
      this.buttonSaveas.Text = "Save as";
      this.buttonSaveas.UseVisualStyleBackColor = true;
      this.buttonSaveas.Click += new EventHandler(this.buttonSaveas_Click);
      this.textBoxVolumeName.Location = new Point(116, 57);
      this.textBoxVolumeName.Name = "textBoxVolumeName";
      this.textBoxVolumeName.Size = new Size(140, 21);
      this.textBoxVolumeName.TabIndex = 3;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(21, 60);
      this.label3.Name = "label3";
      this.label3.Size = new Size(77, 12);
      this.label3.TabIndex = 6;
      this.label3.Text = "Volume Name:";
      this.buttonCreate.Location = new Point(261, 227);
      this.buttonCreate.Name = "buttonCreate";
      this.buttonCreate.Size = new Size(75, 23);
      this.buttonCreate.TabIndex = 8;
      this.buttonCreate.Text = "Create";
      this.buttonCreate.UseVisualStyleBackColor = true;
      this.buttonCreate.Click += new EventHandler(this.buttonCreate_Click);
      this.groupBox.Controls.Add((Control) this.labelStatus);
      this.groupBox.Controls.Add((Control) this.progressBar);
      this.groupBox.Location = new Point(23, 133);
      this.groupBox.Name = "groupBox";
      this.groupBox.Size = new Size(399, 82);
      this.groupBox.TabIndex = 6;
      this.groupBox.TabStop = false;
      this.groupBox.Text = "Progress";
      this.labelStatus.Location = new Point(18, 55);
      this.labelStatus.Name = "labelStatus";
      this.labelStatus.Size = new Size(359, 17);
      this.labelStatus.TabIndex = 10;
      this.progressBar.Location = new Point(20, 22);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new Size(357, 21);
      this.progressBar.TabIndex = 7;
      this.linkLabel.AutoSize = true;
      this.linkLabel.Location = new Point(21, 232);
      this.linkLabel.Name = "linkLabel";
      this.linkLabel.Size = new Size(137, 12);
      this.linkLabel.TabIndex = 0;
      this.linkLabel.TabStop = true;
      this.linkLabel.Text = "www.freeisocreator.com";
      this.linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
      this.buttonClose.Location = new Point(347, 227);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new Size(75, 23);
      this.buttonClose.TabIndex = 9;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new EventHandler(this.buttonClose_Click);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(444, 262);
      this.Controls.Add((Control) this.buttonClose);
      this.Controls.Add((Control) this.linkLabel);
      this.Controls.Add((Control) this.groupBox);
      this.Controls.Add((Control) this.buttonCreate);
      this.Controls.Add((Control) this.textBoxVolumeName);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.buttonSaveas);
      this.Controls.Add((Control) this.textBoxISOFile);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.buttonBrowse);
      this.Controls.Add((Control) this.textBoxSourceFolder);
      this.Controls.Add((Control) this.label1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (MainForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Free ISO Creator";
      this.groupBox.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private delegate void SetLabelDelegate(string text);

    private delegate void SetNumericValueDelegate(int value);
  }
}
