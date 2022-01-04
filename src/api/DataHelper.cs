using System.Data.SQLite;

public static class DataHelper{
    public static void CreateData(){
    string cs = @"URI=file:storage.db";

    using var con = new SQLiteConnection(cs);
    con.Open();

    using var cmd = new SQLiteCommand(con);

    cmd.CommandText = "DROP TABLE IF EXISTS CustomerInfo";
    cmd.ExecuteNonQuery();

    cmd.CommandText = @"CREATE TABLE CustomerInfo(id INTEGER PRIMARY KEY,
                Name TEXT, City TEXT)";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City) VALUES('John','Istanbul')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City) VALUES('Jam', 'Ankara')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City) VALUES('Sam','Izmir')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City) VALUES('Pam', 'New Orleans')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City) VALUES('Michael', 'New York')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City) VALUES('Jannet', 'New Jersey')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City) VALUES('Jason', 'New Holland')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City) VALUES('Melbourne', 'Baku')";
    cmd.ExecuteNonQuery();

    Console.WriteLine("Table cars created");

        cmd.CommandText = @"SELECT * FROM CustomerInfo";
        cmd.ExecuteNonQuery();

    }
}