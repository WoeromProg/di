using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App
{
    public class MainForm : Form
    {
        public MainForm(
            IUiAction[] actions,
            Palette palette,
            PictureBoxImageHolder imageHolder,
            ImageSettings imageSettings)
        {
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.OrderBy(e => e.Category).ToArray().ToMenuItems());
            Controls.Add(mainMenu);
            
            imageHolder.RecreateImage(imageSettings);
            imageHolder.Dock = DockStyle.Fill;
            Controls.Add(imageHolder);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Fractal Painter";
        }
    }
}