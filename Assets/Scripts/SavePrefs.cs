//[System.Serializable]
//public class SavePrefs
//{
//    public readonly string Email;
//    public readonly string Password;

//    public SavePrefs()
//    {

//    }

//    public SavePrefs(string _Email, string _Password)
//    {
//        Email = _Email;
//        Password = _Password;
//    }

//    private void SaveData()
//    {
//        SavePrefs dataStorer = new SavePrefs();// aqui metemos los datos
//        .... // some change in his data
//        string json = JsonUtility.ToJson(this, true);//true for you can read the file
//        path = Path.Combine(Application.persistantDataPath, "saved files", "data.json");
//        File.WriteAllText(path, json);
//    }

//    private void ReadData()
//    {
//        string json = File.ReadAllText(path);
//        SavePrefs dataStorer = new SavePrefs();
//        JsonUtility.FromJsonOverwrite(json, dataStorer);
//    }

//}

