using SQLite;

namespace TodoSQLite.Models;

public class Persoon
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string VoorNaam { get; set; }
    public string AchterNaam { get; set; }
    public string Afbeelding { get; set; }
    public string TelefoonNummer { get; set; }
    public string Geslacht { get; set; }
    public bool IsMan { get; set; }
    public bool IsVrouw { get; set; }
    public bool IsOnbekend { get; set; }

}
