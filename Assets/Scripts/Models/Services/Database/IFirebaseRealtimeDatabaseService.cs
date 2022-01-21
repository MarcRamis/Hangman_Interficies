using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFirebaseRealtimeDatabaseService
{
    void ReadDataBase();
    Task ReadDataBase1();
    void OrderedListByScore();
    Task OrderedListByScoreByTask();
    void OrderedListByPosition();
    Task OrderedListByPositionByTask();
    Task SaveScore(ScoreUserPrefs scoreUser);
    Task SaveUserID(UserEntity user);
    Task SaveUserName(ScoreUserPrefs scoreUser);
}
