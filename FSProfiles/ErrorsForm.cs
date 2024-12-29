using FSProfiles.Common.Classes;

namespace FSProfiles
{
    public partial class ErrorsForm : Form
    {
        public ErrorsForm()
        {
            InitializeComponent();
        }

        public void SetErrors(List<ProfileError> errorList)
        {
            var errorMsg = errorList.Aggregate("", (current, error) => current + $"Profile File: {error.ProfileFile}\r\n\t{error.Error}\r\n\r\n");
            txtErrorList.Text = errorMsg;
        }
    }

}
