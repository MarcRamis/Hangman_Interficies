using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFirebaseRealtimeDatabaseService
{
    void ReadDataBase();
    Task ReadDataBase1();
    void OrderedListByScore();
    void OrderedListByPosition();
}
