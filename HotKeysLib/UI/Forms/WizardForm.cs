using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for WizardForm.
	/// </summary>
	public class WizardForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel wizardPanel1;
		private System.Windows.Forms.Button backButton;
		private System.Windows.Forms.Button nextButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox fileTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton commandLineItemRadioButton;
		private System.Windows.Forms.RadioButton addInRadioButton;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.ComboBox addInComboBox;
		private System.Windows.Forms.ComboBox actionComboBox;
		private System.Windows.Forms.Button configureAddInButton;
		private System.Windows.Forms.Button configureActionButton;
		private System.Windows.Forms.Label commandLineLabel;
		private System.Windows.Forms.Label addInLabel;
		private System.Windows.Forms.Panel wizardPanel2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox keyComboBox;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WizardForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.configureActionButton.Enabled = false;
			this.configureAddInButton.Enabled = false;
			this.nextButton.Enabled = false;
			ArrayList addIns = HotKeyAddInManager.GetAllAddIns();
			foreach(IHotKeysAddIn addIn in addIns)
			{
				this.addInComboBox.Items.Add(addIn);
			}
			if(addIns.Count<=0)
				this.addInComboBox.SelectedIndex = 0;
			foreach(EnhancedKey key in EnhancedKey.GetAvailableEnhancedKeys())
			{
				this.keyComboBox.Items.Add(key);
			}
			this.keyComboBox.SelectedIndex = 0;
			this.backButton.Enabled = false;
			this.commandLineItemRadioButton.Checked = true;
			this.wizardPanel2.Left = this.wizardPanel1.Left;
			this.wizardPanel2.Enabled = false;
			this.wizardPanel2.Visible = false;
			this.enableDisableStuff();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WizardForm));
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.nextButton = new System.Windows.Forms.Button();
			this.backButton = new System.Windows.Forms.Button();
			this.wizardPanel1 = new System.Windows.Forms.Panel();
			this.addInLabel = new System.Windows.Forms.Label();
			this.commandLineLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.configureActionButton = new System.Windows.Forms.Button();
			this.configureAddInButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.actionComboBox = new System.Windows.Forms.ComboBox();
			this.addInComboBox = new System.Windows.Forms.ComboBox();
			this.browseButton = new System.Windows.Forms.Button();
			this.fileTextBox = new System.Windows.Forms.TextBox();
			this.addInRadioButton = new System.Windows.Forms.RadioButton();
			this.commandLineItemRadioButton = new System.Windows.Forms.RadioButton();
			this.wizardPanel2 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.keyComboBox = new System.Windows.Forms.ComboBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.wizardPanel1.SuspendLayout();
			this.wizardPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(0, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(168, 293);
			this.pictureBox2.TabIndex = 5;
			this.pictureBox2.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cancelButton);
			this.groupBox1.Controls.Add(this.nextButton);
			this.groupBox1.Controls.Add(this.backButton);
			this.groupBox1.Location = new System.Drawing.Point(0, 288);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(616, 100);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cancelButton.Location = new System.Drawing.Point(400, 16);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// nextButton
			// 
			this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.nextButton.Location = new System.Drawing.Point(312, 16);
			this.nextButton.Name = "nextButton";
			this.nextButton.TabIndex = 1;
			this.nextButton.Text = "Next >";
			this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
			// 
			// backButton
			// 
			this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.backButton.Location = new System.Drawing.Point(232, 16);
			this.backButton.Name = "backButton";
			this.backButton.TabIndex = 0;
			this.backButton.Text = "< Back";
			this.backButton.Click += new System.EventHandler(this.backButton_Click);
			// 
			// wizardPanel1
			// 
			this.wizardPanel1.BackColor = System.Drawing.Color.White;
			this.wizardPanel1.Controls.Add(this.addInLabel);
			this.wizardPanel1.Controls.Add(this.commandLineLabel);
			this.wizardPanel1.Controls.Add(this.label2);
			this.wizardPanel1.Controls.Add(this.configureActionButton);
			this.wizardPanel1.Controls.Add(this.configureAddInButton);
			this.wizardPanel1.Controls.Add(this.label1);
			this.wizardPanel1.Controls.Add(this.actionComboBox);
			this.wizardPanel1.Controls.Add(this.addInComboBox);
			this.wizardPanel1.Controls.Add(this.browseButton);
			this.wizardPanel1.Controls.Add(this.fileTextBox);
			this.wizardPanel1.Controls.Add(this.addInRadioButton);
			this.wizardPanel1.Controls.Add(this.commandLineItemRadioButton);
			this.wizardPanel1.Location = new System.Drawing.Point(160, 0);
			this.wizardPanel1.Name = "wizardPanel1";
			this.wizardPanel1.Size = new System.Drawing.Size(336, 293);
			this.wizardPanel1.TabIndex = 7;
			// 
			// addInLabel
			// 
			this.addInLabel.AutoSize = true;
			this.addInLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.addInLabel.Location = new System.Drawing.Point(32, 168);
			this.addInLabel.Name = "addInLabel";
			this.addInLabel.Size = new System.Drawing.Size(287, 16);
			this.addInLabel.TabIndex = 11;
			this.addInLabel.Text = "Trigger extended functionality (ie: clock, volume control):";
			// 
			// commandLineLabel
			// 
			this.commandLineLabel.AutoSize = true;
			this.commandLineLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.commandLineLabel.Location = new System.Drawing.Point(32, 88);
			this.commandLineLabel.Name = "commandLineLabel";
			this.commandLineLabel.Size = new System.Drawing.Size(269, 16);
			this.commandLineLabel.TabIndex = 10;
			this.commandLineLabel.Text = "Start executable or open file using the following path:";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(304, 40);
			this.label2.TabIndex = 9;
			this.label2.Text = "Determine what happens when a HotKey is pressed.";
			// 
			// configureActionButton
			// 
			this.configureActionButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.configureActionButton.Location = new System.Drawing.Point(240, 248);
			this.configureActionButton.Name = "configureActionButton";
			this.configureActionButton.TabIndex = 8;
			this.configureActionButton.Text = "Configure";
			// 
			// configureAddInButton
			// 
			this.configureAddInButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.configureAddInButton.Location = new System.Drawing.Point(240, 192);
			this.configureAddInButton.Name = "configureAddInButton";
			this.configureAddInButton.TabIndex = 7;
			this.configureAddInButton.Text = "Configure";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(32, 224);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Action:";
			// 
			// actionComboBox
			// 
			this.actionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.actionComboBox.Location = new System.Drawing.Point(32, 248);
			this.actionComboBox.Name = "actionComboBox";
			this.actionComboBox.Size = new System.Drawing.Size(200, 21);
			this.actionComboBox.TabIndex = 5;
			this.actionComboBox.SelectedIndexChanged += new System.EventHandler(this.actionComboBox_SelectedIndexChanged);
			// 
			// addInComboBox
			// 
			this.addInComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.addInComboBox.Location = new System.Drawing.Point(32, 192);
			this.addInComboBox.Name = "addInComboBox";
			this.addInComboBox.Size = new System.Drawing.Size(200, 21);
			this.addInComboBox.TabIndex = 4;
			this.addInComboBox.SelectedIndexChanged += new System.EventHandler(this.addInComboBox_SelectedIndexChanged);
			// 
			// browseButton
			// 
			this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.browseButton.Location = new System.Drawing.Point(240, 112);
			this.browseButton.Name = "browseButton";
			this.browseButton.TabIndex = 3;
			this.browseButton.Text = "Browse";
			this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
			// 
			// fileTextBox
			// 
			this.fileTextBox.Location = new System.Drawing.Point(32, 112);
			this.fileTextBox.Name = "fileTextBox";
			this.fileTextBox.Size = new System.Drawing.Size(200, 20);
			this.fileTextBox.TabIndex = 2;
			this.fileTextBox.Text = "";
			this.fileTextBox.TextChanged += new System.EventHandler(this.fileTextBox_TextChanged);
			// 
			// addInRadioButton
			// 
			this.addInRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.addInRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.addInRadioButton.Location = new System.Drawing.Point(16, 144);
			this.addInRadioButton.Name = "addInRadioButton";
			this.addInRadioButton.TabIndex = 1;
			this.addInRadioButton.Text = "Add in";
			this.addInRadioButton.CheckedChanged += new System.EventHandler(this.addInRadioButton_CheckedChanged);
			// 
			// commandLineItemRadioButton
			// 
			this.commandLineItemRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.commandLineItemRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.commandLineItemRadioButton.Location = new System.Drawing.Point(16, 64);
			this.commandLineItemRadioButton.Name = "commandLineItemRadioButton";
			this.commandLineItemRadioButton.Size = new System.Drawing.Size(136, 24);
			this.commandLineItemRadioButton.TabIndex = 0;
			this.commandLineItemRadioButton.Text = "Command line item";
			this.commandLineItemRadioButton.CheckedChanged += new System.EventHandler(this.commandLineItemRadioButton_CheckedChanged);
			// 
			// wizardPanel2
			// 
			this.wizardPanel2.BackColor = System.Drawing.Color.White;
			this.wizardPanel2.Controls.Add(this.label3);
			this.wizardPanel2.Controls.Add(this.label4);
			this.wizardPanel2.Controls.Add(this.keyComboBox);
			this.wizardPanel2.Controls.Add(this.nameTextBox);
			this.wizardPanel2.Location = new System.Drawing.Point(512, 0);
			this.wizardPanel2.Name = "wizardPanel2";
			this.wizardPanel2.Size = new System.Drawing.Size(336, 293);
			this.wizardPanel2.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(16, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(168, 16);
			this.label3.TabIndex = 11;
			this.label3.Text = "Select a key for the new HotKey:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(16, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(172, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Type a name for the new HotKey:";
			// 
			// keyComboBox
			// 
			this.keyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.keyComboBox.Location = new System.Drawing.Point(16, 104);
			this.keyComboBox.Name = "keyComboBox";
			this.keyComboBox.Size = new System.Drawing.Size(296, 21);
			this.keyComboBox.TabIndex = 4;
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(16, 40);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(296, 20);
			this.nameTextBox.TabIndex = 2;
			this.nameTextBox.Text = "";
			this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
			// 
			// WizardForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(490, 334);
			this.Controls.Add(this.wizardPanel2);
			this.Controls.Add(this.wizardPanel1);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WizardForm";
			this.ShowInTaskbar = false;
			this.Text = "New HotKey Wizard";
			this.Click += new System.EventHandler(this.WizardForm_Click);
			this.Load += new System.EventHandler(this.WizardForm_Load);
			this.Deactivate += new System.EventHandler(this.WizardForm_Deactivate);
			this.groupBox1.ResumeLayout(false);
			this.wizardPanel1.ResumeLayout(false);
			this.wizardPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void WizardForm_Load(object sender, System.EventArgs e)
		{

		}

		public void SetKey(Keys key)
		{
			this.keyComboBox.SelectedItem = EnhancedKey.GetEnhancedKey(key);
		}

		private void commandLineItemRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			this.enableDisableStuff();
		}

		private void addInRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			this.enableDisableStuff();
		}

		private void enableDisableStuff()
		{
			this.fileTextBox.Enabled = this.commandLineItemRadioButton.Checked;
			this.browseButton.Enabled = this.commandLineItemRadioButton.Checked;
			this.commandLineLabel.Enabled = this.commandLineItemRadioButton.Checked;
			this.addInComboBox.Enabled = this.addInRadioButton.Checked;
			this.actionComboBox.Enabled = this.addInRadioButton.Checked;
			//this.configureActionButton.Enabled = this.addInRadioButton.Checked;
			//this.configureAddInButton.Enabled = this.addInRadioButton.Checked;
			this.addInLabel.Enabled = this.addInRadioButton.Checked;
			if(this.addInComboBox.Items.Count > 0 && this.addInComboBox.SelectedIndex < 0)
			{
				this.addInComboBox.SelectedIndex = 0;
			}
			if(this.addInRadioButton.Checked)
				this.nextButton.Enabled = !(this.actionComboBox.Text=="");
			if(this.commandLineItemRadioButton.Checked)
				this.nextButton.Enabled = !(this.fileTextBox.Text=="");
		}

		private void nextButton_Click(object sender, System.EventArgs e)
		{
			if(!this.wizardPanel2.Visible)
			{
				if(this.nameTextBox.Text=="")
				{
					this.nameTextBox.Text = newHotKey.Name;
				}
				this.backButton.Enabled = true;
				this.nextButton.Text = "Finish";
				this.wizardPanel2.Enabled = true;
				this.wizardPanel2.Visible = true;
				this.wizardPanel1.Visible = false;
				this.wizardPanel1.Enabled = false;
			}
			else
			{
				try
				{
					newHotKey.Name = this.nameTextBox.Text;
				}
				catch(DuplicateHotKeyNameException err)
				{
					MessageBox.Show("The name for the HotKey allready exists. The name must be unique.", "Duplicate name", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.nameTextBox.Text = newHotKey.Name;
					this.nameTextBox.Focus();
					return;
				}
				catch(EmptyHotKeyNameException err)
				{
					MessageBox.Show("The name cannot be empty.", "Empty name", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.nameTextBox.Text = newHotKey.Name;
					this.nameTextBox.Focus();
					return;
				}
				newHotKey.Key = ((EnhancedKey)this.keyComboBox.SelectedItem).Key;
				if(this.commandLineItemRadioButton.Checked)
				{
					if(System.IO.Directory.Exists(this.fileTextBox.Text))
						newHotKey.TargetType = HotKeyTargetType.Folder;
					else
						newHotKey.TargetType = HotKeyTargetType.File;
					newHotKey.Target = this.fileTextBox.Text;
				}
				else
				{
					newHotKey.TargetType = HotKeyTargetType.AddIn;
					HotKeyConfiguredAddInAction actionInst = new HotKeyConfiguredAddInAction();
					//actionInst.AddInName = ((IHotKeysAddIn)this.addInComboBox.SelectedItem).AddInName;
					actionInst.AddInID = ((IHotKeysAddIn)this.addInComboBox.SelectedItem).AddInID;
					actionInst.ActionID = ((HotKeyAddInAction)this.actionComboBox.SelectedItem).ID;
					newHotKey.Target = actionInst;
					// TODO: figure out a way to add action config
				}
				newHotKey.Enabled = true;
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		HotKey newHotKey = new HotKey();
		public HotKey NewHotKey
		{
			get
			{
				return newHotKey;
			}
		}

		private void backButton_Click(object sender, System.EventArgs e)
		{
			this.backButton.Enabled = false;
			this.nextButton.Text = "Next >";
			this.nextButton.Enabled = true;
			this.wizardPanel1.Enabled = true;
			this.wizardPanel1.Visible = true;
			this.wizardPanel2.Visible = false;
			this.wizardPanel2.Enabled = false;
		}

		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void browseButton_Click(object sender, System.EventArgs e)
		{
			FileSytemItemSelector dirDialog = new FileSytemItemSelector();
			dirDialog.SelectType = FileSytemItemSelector.FileSystemItemTypes.FilesAndDirectories;
			dirDialog.Title = "Select a target for your new HotKey.";
			if(dirDialog.ShowDialog(this) == DialogResult.OK)
				if(dirDialog.Selected != "")
					this.fileTextBox.Text = dirDialog.Selected;
		}

		private void addInComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			IHotKeysAddIn addIn = (IHotKeysAddIn)this.addInComboBox.SelectedItem;
			this.configureAddInButton.Enabled = addIn.HasConfig;
			this.actionComboBox.Items.Clear();
			foreach(HotKeyAddInAction action in addIn.Actions)
			{
				this.actionComboBox.Items.Add(action);
			}
			if(addIn.Actions.Count>0)
				this.actionComboBox.SelectedIndex = 0;
		}

		private void actionComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			HotKeyAddInAction action = (HotKeyAddInAction)this.actionComboBox.SelectedItem;
			this.configureActionButton.Enabled = action.HasConfig;
			if(this.addInRadioButton.Checked)
				this.nextButton.Enabled = !(this.actionComboBox.Text=="");
		}

		private void fileTextBox_TextChanged(object sender, System.EventArgs e)
		{
			if(this.commandLineItemRadioButton.Checked)
				this.nextButton.Enabled = !(this.fileTextBox.Text=="");
		}

		private void nameTextBox_TextChanged(object sender, System.EventArgs e)
		{
			this.nextButton.Enabled = !(this.nameTextBox.Text=="");
		}

		private void WizardForm_Click(object sender, System.EventArgs e)
		{
			this.TopMost = true;
//			this.Focus();
		}

		private void WizardForm_Deactivate(object sender, System.EventArgs e)
		{
			this.TopMost = false;
		}
	}
}
