namespace EMR
{
    public partial class ImageViewerForm : Form
    {
        private byte[] imageData;
        public ImageViewerForm(byte[] imageData)
        {
            InitializeComponent();
            this.imageData = imageData;
        }

        private void ImageViewerForm_Load(object sender, EventArgs e)
        {
            if (imageData != null && imageData.Length > 0)
            {
                using (var ms = new System.IO.MemoryStream(imageData))
                {
                    Image image = Image.FromStream(ms);
                    pictureBox.Image = image;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
