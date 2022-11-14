using System.Security.Cryptography;
using System.Text.Json;

namespace WebIsland;

public class MCHandler
{
    private List<MathematicalCalculations> _notes = new();

    private JsonSerializerOptions _options;
    
    public MCHandler()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new MCSerialiser());
    }
    
    public async Task GetAllMC(HttpResponse response)
    {
        await response.WriteAsJsonAsync(_notes, _options);
    }

    public async Task CreateNewNote(HttpRequest request, HttpResponse response)
    {
        try
        {
            var note = await request.ReadFromJsonAsync<MathematicalCalculations>(_options);
            if (note is not null)
            {
                note.Id = RandomNumberGenerator.GetInt32(0, 512);
                _notes.Add(note);
                await response.WriteAsJsonAsync(note, _options);
            }
            else
            {
                throw new Exception("Incorrect data");
            }

        }
        catch (Exception e)
        {
            response.StatusCode = 400;
            await response.WriteAsJsonAsync(new { message = e.Message });
        }
    }

    public async Task DeleteNote(int? id, HttpResponse response)
    {
        Console.WriteLine("Enter");
        var note = _notes.FirstOrDefault(note => note.Id == id);
        if (note is not null)
        {
            _notes.Remove(note);
            await response.WriteAsJsonAsync(note, _options);
        }
        else
        {
            response.StatusCode = 404;
            await response.WriteAsJsonAsync(new { message = "Note has not been founded" });
        }
    }
}