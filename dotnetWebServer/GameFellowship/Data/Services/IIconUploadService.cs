using Microsoft.AspNetCore.Components.Forms;

namespace GameFellowship.Data.Services
{
    public interface IIconUploadService
	{
		Task<(bool, string)> IconUploadToJpg(InputFileChangeEventArgs e, IWebHostEnvironment Environment, string iconFolder, string fileName, long imageStorageSize = 1024 * 1024 * 3, int imagePixelSize = 500);
	}
}