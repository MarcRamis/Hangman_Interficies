using System.Collections.Generic;

public interface IFirebaseRealtimeDatabaseService
{
    IReadOnlyList<ScoreUserPrefs> GetAll();
    void ReadDataBase();
    void OrderedListByScore();
    void OrderedListByPosition();
}
