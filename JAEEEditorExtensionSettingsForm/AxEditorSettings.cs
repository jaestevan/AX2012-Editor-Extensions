using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JAEE.AX.EditorExtensions
{
    public partial class AxEditorSettings : Form
    {
        private EditorSettings singletonSettings = null;

        #region - SETTINGS -

        private void loadSettings()
        {
            singletonSettings = EditorSettings.getInstance();

            HighlightWordProperties highlightWordProperties = new HighlightWordProperties(singletonSettings.HighlightWord);
            this.propHighlightWord.SelectedObject = highlightWordProperties;

            HighlightLineProperties highlightLineProperties = new HighlightLineProperties(singletonSettings.HighlightCurrentLine);
            this.propHighlightLine.SelectedObject = highlightLineProperties;

            nRows.Value = singletonSettings.Outlining.MaxRowsInTooltip;
        }

        private void saveSettings()
        {
            HighlightWordProperties propHighlightWord = (HighlightWordProperties)this.propHighlightWord.SelectedObject;
            HighlightLineProperties propHighlightLine = (HighlightLineProperties)this.propHighlightLine.SelectedObject;

            singletonSettings = EditorSettings.getInstance();

            // Highliht selected word
            singletonSettings.HighlightWord.BackColor = propHighlightWord.BackColor;
            singletonSettings.HighlightWord.FrameColor = propHighlightWord.FrameColor;

            // Highlight current line
            singletonSettings.HighlightCurrentLine.BackColor = propHighlightLine.BackColor;
            singletonSettings.HighlightCurrentLine.FrameColor = propHighlightLine.FrameColor;
            singletonSettings.HighlightCurrentLine.BackOpacity = propHighlightLine.BackOpacity;

            // Outlining 
            singletonSettings.Outlining.MaxRowsInTooltip = Convert.ToInt32(nRows.Value);
            
            singletonSettings.saveChanges();
        }

        #endregion

        #region - FORM -
        
        public AxEditorSettings()
        {
            InitializeComponent();
        }

        private void AxEditorSettings_Load(object sender, EventArgs e)
        {
            this.loadSettings();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.saveSettings();
            MessageBox.Show("Please reopen Microsot Dynamics AX client to see the changes.");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
