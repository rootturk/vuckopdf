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
                Name TEXT, City TEXT, Address TEXT)";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City, Address) VALUES('John Drake','Istanbul', 'KADIKOY, HALITAGA')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City, Address) VALUES('Jam Penny', 'Ankara','KADIKOY, RASIMPASA')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City, Address) VALUES('Sam Austin','Izmir','KADIKOY, MODA')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City, Address) VALUES('Pam Angel', 'New Orleans','KADIKOY, HALITAGA')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City, Address) VALUES('Michael Dagger', 'New York','KADIKOY, HALITAGA')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City, Address) VALUES('Jannet Behindery', 'New Jersey','KADIKOY, HALITAGA')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City, Address) VALUES('Jason Peaker', 'New Holland','KADIKOY, HALITAGA')";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO CustomerInfo(Name, City, Address) VALUES('Melbourne Keynes', 'Baku','KADIKOY, HALITAGA')";
    cmd.ExecuteNonQuery();

    Console.WriteLine("Table cars created");

    cmd.CommandText = @"SELECT * FROM CustomerInfo";
    cmd.ExecuteNonQuery();

    }
}