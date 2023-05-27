using byteforzaFinalProject.Interface;

namespace byteforzaFinalProject.IRepo
{
	public class FileRepo : IFileInterface
	{
		public async Task<string> saveAndGetFileName(IFormFile file)
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);


			string fileNameWithPath = Path.Combine(path, file.FileName);

			using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}
			return fileNameWithPath;
		}
	}
}
