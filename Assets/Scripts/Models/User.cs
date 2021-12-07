using Firebase.Firestore;

[FirestoreData]
public class User
{
    [FirestoreProperty]
    public string Name { get; set; }

    public User()
    {
    }

    public User(string name)
    {
        Name = name;
    }
}
