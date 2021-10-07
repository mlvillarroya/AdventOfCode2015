namespace AdventOfCode2015.Services
{
    public interface IFileWrapper
    {
        string ReadAllTextInOneString(string path);
        string[] ReadAllTextInArray(string path);
    }
}