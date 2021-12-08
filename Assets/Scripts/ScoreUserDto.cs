using System.Collections.Generic;
using System;

[Serializable]
public class ScoreUserDto
{
    public List<ScoreUserDto> ScoreUsers;

    public ScoreUserDto(List<ScoreUserDto> scoreUsers)
    {
        ScoreUsers = scoreUsers;
    }
}