namespace byteforzaFinalProject.Interface
{
	public interface IFileInterface
	{
		Task<string> saveAndGetFileName(IFormFile file);
	}
}
