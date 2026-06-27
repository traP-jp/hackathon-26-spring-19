using Unity.Microsoft.GDK;
using UnityEngine;
public class TimerSystem
{
    public void Initialize(GameData gameData){
        gameData.elapsedTime = 0f;
        gameData.remainingTime = gameData.timeLimit;
    }

    public void UpdateTimer(GameData gameData, float deltaTime){
        gameData.remainingTime -= deltaTime;
        gameData.elapsedTime += deltaTime;
        if(gameData.remainingTime < 0f){
            gameData.remainingtime = 0f;
        }
    } 
    
    public bool IsTimeUp(GameData gameData){
        if(gameData.remainingTime <= 0f)return true;
        else return false;
    }
}
