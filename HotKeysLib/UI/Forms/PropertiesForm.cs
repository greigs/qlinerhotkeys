using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace HotKeysLib
{
	/// <summary>
	/// Summary description for PropertiesForm.
	/// </summary>
	public class PropertiesForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.TextBox fileTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox keyComboBox;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.ComboBox targetTypeComboBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel addInPanel;
		private System.Windows.Forms.Button configureActionButton;
		private System.Windows.Forms.Button configureAddInButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox actionComboBox;
		private System.Windows.Forms.ComboBox addInComboBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
		private HotKeysLib.Line line1;
		private HotKeysLib.Line line2;
		private HotKeysLib.Line line3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel filePanel;
		private System.Windows.Forms.CheckBox enabledCheckBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button browseFolderButton;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox workingDirTextBox;
		private System.Windows.Forms.TextBox argumentsTextBox;
		private System.Windows.Forms.Label label10;

		private Icon topLeftIcon = null;
		public PropertiesForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			StreamReader streamReader = new StreamReader(Assembly.GetAssembly(typeof(PropertiesForm)).GetManifestResourceStream("HotKeysLib.UI.Icons.keyboard.ico"));
			topLeftIcon = new Icon(streamReader.BaseStream,48,48);
			ArrayList addIns = HotKeyAddInManager.GetAllAddIns();
			foreach(IHotKeysAddIn addIn in addIns)
			{
				this.addInComboBox.Items.Add(addIn);
			}
			if(addIns.Count>0)
				this.addInComboBox.SelectedIndex = 0;
			foreach(EnhancedKey key in EnhancedKey.GetAvailableEnhancedKeys())
			{
				this.keyComboBox.Items.Add(key);
			}
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
			this.browseButton = new System.Windows.Forms.Button();
			this.fileTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.keyComboBox = new System.Windows.Forms.ComboBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.applyButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.targetTypeComboBox = new System.Windows.Forms.ComboBox();
			this.addInPanel = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.configureActionButton = new System.Windows.Forms.Button();
			this.configureAddInButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.actionComboBox = new System.Windows.Forms.ComboBox();
			this.addInComboBox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.line1 = new HotKeysLib.Line();
			this.line2 = new HotKeysLib.Line();
			this.line3 = new HotKeysLib.Line();
			this.label4 = new System.Windows.Forms.Label();
			this.enabledCheckBox = new System.Windows.Forms.CheckBox();
			this.filePanel = new System.Windows.Forms.Panel();
			this.argumentsTextBox = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.workingDirTextBox = new System.Windows.Forms.TextBox();
			this.browseFolderButton = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.addInPanel.SuspendLayout();
			this.filePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// browseButton
			// 
			this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.browseButton.Location = new System.Drawing.Point(256, 7);
			this.browseButton.Name = "browseButton";
			this.browseButton.TabIndex = 8;
			this.browseButton.Text = "Browse";
			this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
			// 
			// fileTextBox
			// 
			this.fileTextBox.Location = new System.Drawing.Point(80, 8);
			this.fileTextBox.Name = "fileTextBox";
			this.fileTextBox.Size = new System.Drawing.Size(168, 20);
			this.fileTextBox.TabIndex = 7;
			this.fileTextBox.Text = "";
			this.fileTextBox.TextChanged += new System.EventHandler(this.textChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(16, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(27, 16);
			this.label3.TabIndex = 30;
			this.label3.Text = "Key:";
			// 
			// keyComboBox
			// 
			this.keyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.keyComboBox.Location = new System.Drawing.Point(80, 80);
			this.keyComboBox.Name = "keyComboBox";
			this.keyComboBox.Size = new System.Drawing.Size(248, 21);
			this.keyComboBox.TabIndex = 1;
			this.keyComboBox.SelectedIndexChanged += new System.EventHandler(this.textChanged);
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(80, 16);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(248, 20);
			this.nameTextBox.TabIndex = 0;
			this.nameTextBox.Text = "";
			this.nameTextBox.TextChanged += new System.EventHandler(this.textChanged);
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.okButton.Location = new System.Drawing.Point(94, 328);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 13;
			this.okButton.Text = "OK";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// applyButton
			// 
			this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.applyButton.Location = new System.Drawing.Point(256, 328);
			this.applyButton.Name = "applyButton";
			this.applyButton.TabIndex = 15;
			this.applyButton.Text = "Apply";
			this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cancelButton.Location = new System.Drawing.Point(176, 328);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 14;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// targetTypeComboBox
			// 
			this.targetTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.targetTypeComboBox.Location = new System.Drawing.Point(80, 136);
			this.targetTypeComboBox.Name = "targetTypeComboBox";
			this.targetTypeComboBox.Size = new System.Drawing.Size(248, 21);
			this.targetTypeComboBox.TabIndex = 2;
			this.targetTypeComboBox.TextChanged += new System.EventHandler(this.textChanged);
			this.targetTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.targetTypeComboBox_SelectedIndexChanged);
			// 
			// addInPanel
			// 
			this.addInPanel.Controls.Add(this.label2);
			this.addInPanel.Controls.Add(this.configureActionButton);
			this.addInPanel.Controls.Add(this.configureAddInButton);
			this.addInPanel.Controls.Add(this.label1);
			this.addInPanel.Controls.Add(this.actionComboBox);
			this.addInPanel.Controls.Add(this.addInComboBox);
			this.addInPanel.Location = new System.Drawing.Point(0, 160);
			this.addInPanel.Name = "addInPanel";
			this.addInPanel.Size = new System.Drawing.Size(352, 72);
			this.addInPanel.TabIndex = 40;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(16, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 16);
			this.label2.TabIndex = 31;
			this.label2.Text = "Add in:";
			// 
			// configureActionButton
			// 
			this.configureActionButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.configureActionButton.Location = new System.Drawing.Point(256, 39);
			this.configureActionButton.Name = "configureActionButton";
			this.configureActionButton.TabIndex = 6;
			this.configureActionButton.Text = "Configure";
			// 
			// configureAddInButton
			// 
			this.configureAddInButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.configureAddInButton.Location = new System.Drawing.Point(256, 7);
			this.configureAddInButton.Name = "configureAddInButton";
			this.configureAddInButton.TabIndex = 4;
			this.configureAddInButton.Text = "Configure";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(15, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 16);
			this.label1.TabIndex = 28;
			this.label1.Text = "Action:";
			// 
			// actionComboBox
			// 
			this.actionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.actionComboBox.Location = new System.Drawing.Point(80, 40);
			this.actionComboBox.Name = "actionComboBox";
			this.actionComboBox.Size = new System.Drawing.Size(168, 21);
			this.actionComboBox.TabIndex = 5;
			this.actionComboBox.SelectedIndexChanged += new System.EventHandler(this.actionComboBox_SelectedIndexChanged);
			// 
			// addInComboBox
			// 
			this.addInComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.addInComboBox.Location = new System.Drawing.Point(80, 8);
			this.addInComboBox.Name = "addInComboBox";
			this.addInComboBox.Size = new System.Drawing.Size(168, 21);
			this.addInComboBox.TabIndex = 3;
			this.addInComboBox.SelectedIndexChanged += new System.EventHandler(this.addInComboBox_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label5.Location = new System.Drawing.Point(16, 140);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(33, 16);
			this.label5.TabIndex = 41;
			this.label5.Text = "Type:";
			// 
			// line1
			// 
			this.line1.Location = new System.Drawing.Point(16, 120);
			this.line1.Name = "line1";
			this.line1.Size = new System.Drawing.Size(312, 2);
			this.line1.TabIndex = 42;
			this.line1.TabStop = false;
			// 
			// line2
			// 
			this.line2.Location = new System.Drawing.Point(16, 64);
			this.line2.Name = "line2";
			this.line2.Size = new System.Drawing.Size(312, 2);
			this.line2.TabIndex = 43;
			this.line2.TabStop = false;
			// 
			// line3
			// 
			this.line3.Location = new System.Drawing.Point(17, 272);
			this.line3.Name = "line3";
			this.line3.Size = new System.Drawing.Size(312, 2);
			this.line3.TabIndex = 44;
			this.line3.TabStop = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(16, 284);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(49, 16);
			this.label4.TabIndex = 45;
			this.label4.Text = "Enabled:";
			// 
			// enabledCheckBox
			// 
			this.enabledCheckBox.Location = new System.Drawing.Point(72, 280);
			this.enabledCheckBox.Name = "enabledCheckBox";
			this.enabledCheckBox.Size = new System.Drawing.Size(16, 24);
			this.enabledCheckBox.TabIndex = 12;
			this.enabledCheckBox.Text = "7";
			this.enabledCheckBox.CheckedChanged += new System.EventHandler(this.textChanged);
			// 
			// filePanel
			// 
			this.filePanel.Controls.Add(this.argumentsTextBox);
			this.filePanel.Controls.Add(this.label10);
			this.filePanel.Controls.Add(this.workingDirTextBox);
			this.filePanel.Controls.Add(this.browseFolderButton);
			this.filePanel.Controls.Add(this.label8);
			this.filePanel.Controls.Add(this.label7);
			this.filePanel.Controls.Add(this.browseButton);
			this.filePanel.Controls.Add(this.fileTextBox);
			this.filePanel.Location = new System.Drawing.Point(384, 160);
			this.filePanel.Name = "filePanel";
			this.filePanel.Size = new System.Drawing.Size(352, 104);
			this.filePanel.TabIndex = 47;
			// 
			// argumentsTextBox
			// 
			this.argumentsTextBox.Location = new System.Drawing.Point(80, 72);
			this.argumentsTextBox.Name = "argumentsTextBox";
			this.argumentsTextBox.Size = new System.Drawing.Size(168, 20);
			this.argumentsTextBox.TabIndex = 11;
			this.argumentsTextBox.Text = "";
			this.argumentsTextBox.TextChanged += new System.EventHandler(this.textChanged);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label10.Location = new System.Drawing.Point(16, 76);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(62, 16);
			this.label10.TabIndex = 33;
			this.label10.Text = "Arguments:";
			// 
			// workingDirTextBox
			// 
			this.workingDirTextBox.Location = new System.Drawing.Point(80, 40);
			this.workingDirTextBox.Name = "workingDirTextBox";
			this.workingDirTextBox.Size = new System.Drawing.Size(168, 20);
			this.workingDirTextBox.TabIndex = 9;
			this.workingDirTextBox.Text = "";
			this.workingDirTextBox.TextChanged += new System.EventHandler(this.textChanged);
			// 
			// browseFolderButton
			// 
			this.browseFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.browseFolderButton.Location = new System.Drawing.Point(256, 39);
			this.browseFolderButton.Name = "browseFolderButton";
			this.browseFolderButton.TabIndex = 10;
			this.browseFolderButton.Text = "Browse";
			this.browseFolderButton.Click += new System.EventHandler(this.browseFolderButton_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label8.Location = new System.Drawing.Point(16, 44);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(43, 16);
			this.label8.TabIndex = 30;
			this.label8.Text = "Start in:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label7.Location = new System.Drawing.Point(16, 12);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(31, 16);
			this.label7.TabIndex = 29;
			this.label7.Text = "Path:";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(0, 0);
			this.label9.Name = "label9";
			this.label9.TabIndex = 0;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(0, 0);
			this.label6.Name = "label6";
			this.label6.TabIndex = 0;
			// 
			// PropertiesForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(346, 360);
			this.ControlBox = false;
			this.Controls.Add(this.filePanel);
			this.Controls.Add(this.enabledCheckBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.line3);
			this.Controls.Add(this.line2);
			this.Controls.Add(this.line1);
			this.Controls.Add(this.targetTypeComboBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.keyComboBox);
			this.Controls.Add(this.addInPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PropertiesForm";
			this.ShowInTaskbar = false;
			this.Text = "HotKey Properties";
			this.Click += new System.EventHandler(this.PropertiesForm_Click);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.PropertiesForm_Closing);
			this.Load += new System.EventHandler(this.PropertiesForm_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.PropertiesForm_Paint);
			this.Deactivate += new System.EventHandler(this.PropertiesForm_Deactivate);
			this.addInPanel.ResumeLayout(false);
			this.filePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		public bool ItemChanged
		{
			get
			{
				return this.applyButton.Enabled;
			}
		}

		private enum groupBoxItems {CommandLineItem,AddIn,System};

		private HotKey hotKey = null;

		private void targetTypeComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch(this.targetTypeComboBox.SelectedIndex)
			{
				case (int)groupBoxItems.AddIn:
					this.addInPanel.Left = 0;
					this.filePanel.Left = this.Width + 10;
					this.addInPanel.Enabled = true;
					this.filePanel.Enabled = false;
					break;
				case (int)groupBoxItems.System:
					this.filePanel.Left = this.Width + 10;
					this.addInPanel.Left = this.Width + 10;
					this.addInPanel.Enabled = false;
					this.filePanel.Enabled = false;
					break;
				case (int)groupBoxItems.CommandLineItem:
					this.filePanel.Left = 0;
					this.addInPanel.Left = this.Width + 10;
					this.filePanel.Enabled = true;
					this.addInPanel.Enabled = false;
					break;
			}
			this.updateIcon();
			this.applyButton.Enabled = true;
		}

		private void PropertiesForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawIcon(topLeftIcon,16,8);
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
			this.applyButton.Enabled = true;
			this.updateIcon();
		}

		private void actionComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			HotKeyAddInAction action = (HotKeyAddInAction)this.actionComboBox.SelectedItem;
			this.configureActionButton.Enabled = action.HasConfig;
			this.applyButton.Enabled = true;
			this.updateIcon();
		}
	
		public HotKey HotKey
		{
			get
			{
				return hotKey;
			}
			set
			{
				hotKey = value;
				this.nameTextBox.Text = hotKey.Name;
				this.keyComboBox.Items.Clear();
				this.keyComboBox.Items.AddRange(EnhancedKey.GetAvailableEnhancedKeys(hotKey.Key).ToArray());
				this.keyComboBox.SelectedItem = EnhancedKey.GetEnhancedKey(hotKey.Key);
				this.enabledCheckBox.Checked = hotKey.Enabled;
				switch(hotKey.TargetType)
				{
					case HotKeyTargetType.File:
					case HotKeyTargetType.Folder:
						this.fileTextBox.Text = hotKey.Target.ToString();
						this.argumentsTextBox.Text = hotKey.Arguments;
						this.workingDirTextBox.Text = hotKey.WorkingDir;
						this.targetTypeComboBox.Items.Add("Command line item");
						this.targetTypeComboBox.Items.Add("Add in");
						this.targetTypeComboBox.SelectedIndex = (int)groupBoxItems.CommandLineItem;
						break;
					case HotKeyTargetType.AddIn:
						foreach(IHotKeysAddIn addIn in this.addInComboBox.Items)
						{
							if(addIn.AddInID ==	((HotKeyConfiguredAddInAction)hotKey.Target).AddInID)
							{
								this.addInComboBox.SelectedItem = addIn;
								break;
							}
						}
						this.targetTypeComboBox.Items.Add("Command line item");
						this.targetTypeComboBox.Items.Add("Add in");
						this.targetTypeComboBox.SelectedIndex = (int)groupBoxItems.AddIn;
						break;
					case HotKeyTargetType.System:
						this.enabledCheckBox.Checked = true;
						this.enabledCheckBox.Enabled = false;
						this.targetTypeComboBox.Items.Add("Command line item");
						this.targetTypeComboBox.Items.Add("Add in");
						this.targetTypeComboBox.Items.Add("System");
						this.nameTextBox.Enabled = false;
						this.keyComboBox.Enabled = false;
						this.targetTypeComboBox.Enabled = false;
						this.targetTypeComboBox.SelectedIndex = (int)groupBoxItems.System;
						break;
				}
				this.updateIcon();
				this.applyButton.Enabled = false;
			}
		}

        // TODO: remove this function by integrating it in save HotKey (or better: remove validing code from
		// properties form and put it in the HotKeys Class
		private bool isHotKeyValid()
		{
			try
			{
				HotKeyHelperFunctions.ValidateHotKeyName( this.nameTextBox.Text , this.hotKey );
			}
			catch(DuplicateHotKeyNameException err)
			{
				MessageBox.Show("The name for the HotKey allready exists. The name must be unique.", "Duplicate name", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.nameTextBox.Text = hotKey.Name;
				this.nameTextBox.Focus();
				return false;
			}
			catch(EmptyHotKeyNameException err)
			{
				MessageBox.Show("The name cannot be empty.", "Empty name", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.nameTextBox.Text = hotKey.Name;
				this.nameTextBox.Focus();
				return false;
			}
			if(this.fileTextBox.Text=="" && this.targetTypeComboBox.SelectedIndex==(int)groupBoxItems.CommandLineItem)
			{
				MessageBox.Show(this,"The item path is empty.","No path",MessageBoxButtons.OK,MessageBoxIcon.Information);
				this.fileTextBox.Focus();
				return false;
			}
			return true;
		}

		private void saveHotKey()
		{
			hotKey.Name = this.nameTextBox.Text;
			hotKey.Key = ((EnhancedKey)this.keyComboBox.SelectedItem).Key;
			if (this.targetTypeComboBox.SelectedIndex==(int)groupBoxItems.CommandLineItem)
			{
				if(System.IO.Directory.Exists(this.fileTextBox.Text))
					hotKey.TargetType = HotKeyTargetType.Folder;
				else
					hotKey.TargetType = HotKeyTargetType.File;
				hotKey.Target = this.fileTextBox.Text;
				hotKey.WorkingDir = this.workingDirTextBox.Text;
				hotKey.Arguments = this.argumentsTextBox.Text;
			}
			else
			{
				hotKey.TargetType = HotKeyTargetType.AddIn;
				hotKey.Target = newHotKeyAction();
			}
			hotKey.Enabled = this.enabledCheckBox.Checked;
		}

		private HotKeyConfiguredAddInAction newHotKeyAction()
		{
			HotKeyConfiguredAddInAction actionInst = new HotKeyConfiguredAddInAction();
			actionInst.AddInID = ((IHotKeysAddIn)this.addInComboBox.SelectedItem).AddInID;
			actionInst.ActionID = ((HotKeyAddInAction)this.actionComboBox.SelectedItem).ID;
			// TODO: figure out a way to add action config
			return actionInst;
		}

		bool abortClose = false;
		private void okButton_Click(object sender, System.EventArgs e)
		{
			if(hotKey.TargetType!=HotKeyTargetType.System)
			{
				if(isHotKeyValid())
				{
					try
					{
						this.saveHotKey();
						this.Close();
					}
					catch
					{
						return;
					}
				}
				else
				{
					abortClose = true;
				}	
			}
		}

		private void applyButton_Click(object sender, System.EventArgs e)
		{
			if(hotKey.TargetType!=HotKeyTargetType.System)
			{
				if(isHotKeyValid())
				{
					try
					{
						this.saveHotKey();
						if(this.Apply!=null)
							this.Apply(this, new EventArgs());
						this.applyButton.Enabled = false;
					}
					catch
					{
						return;	
					}
				}
			}		
		}

		public EventHandler Apply;

		private void textChanged(object sender, System.EventArgs e)
		{
			this.applyButton.Enabled = true;
			if(sender==this.fileTextBox)
				updateIcon();
		}

		private void updateIcon()
		{
			switch(this.targetTypeComboBox.SelectedIndex)
			{
				case (int)groupBoxItems.CommandLineItem:
					topLeftIcon = HotKeyHelperFunctions.GetIconForHotKeyTarget(this.fileTextBox.Text);
					break;
				case (int)groupBoxItems.AddIn:
					topLeftIcon = HotKeyHelperFunctions.GetIconForHotKeyTarget(this.newHotKeyAction());
					break;
				case (int)groupBoxItems.System:
					topLeftIcon = hotKey.Icon; //HotKeyHelperFunctions.GetIconForHotKeyTarget("");
					break;
			}
			if(this.Visible)
			{			
				this.Refresh();
			}
		}

		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void PropertiesForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(this.abortClose)
			{
				e.Cancel = true;
				abortClose = false;
			}
		}

		private void PropertiesForm_Deactivate(object sender, System.EventArgs e)
		{
			this.TopMost = false;
		}

		private void PropertiesForm_Click(object sender, System.EventArgs e)
		{
			this.TopMost = true;
			// this.Focus();
		}

		private void PropertiesForm_Load(object sender, System.EventArgs e)
		{
		
		}

		private void browseFolderButton_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Select start in folder.";
			if(dialog.ShowDialog()==DialogResult.OK)
			{
				this.workingDirTextBox.Text = dialog.SelectedPath;
			}
		}
	}
}
