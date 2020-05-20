import javafx.util.Pair;

import java.util.HashMap;
import java.util.UUID;

public class GameManager {
    private static String waitingPlayer = null;
    private static HashMap<UUID, GameRoom> GameRooms = new HashMap<>();

    public static void addPlayerToQueue(String playerName) {
        if (waitingPlayer != null && !waitingPlayer.equals(playerName)) {
            GameRoom room = new GameRoom(waitingPlayer, playerName);
            GameRooms.put(room.getId(), room);
            waitingPlayer = null;
        } else {
            waitingPlayer = playerName;
        }
    }


    public static void shoot(Pair<Integer, Integer> point, String playerName){
        try {
            GameRooms.get(gameKeyForPlayer(playerName)).doShoot(point, playerName);
        }
        catch (Exception ex){

        }
    }

    public static boolean isNowCurrentStepOf(String playerName){
        try {
            return GameRooms.get(gameKeyForPlayer(playerName)).isNowCurrentStepOf(playerName);
        }
        catch (Exception ex){
            return false;
        }
    }

    public static String getGameData(String playerName){
        try {
            return GameRooms.get(gameKeyForPlayer(playerName)).getGameData(playerName);
        }
        catch (Exception ex){
            return "There is no Game for you!";

        }
    }

    public static boolean gameFinishedForUser(String playerName){
        try {
            return GameRooms.get(gameKeyForPlayer(playerName)).gameFinished();
        }
        catch (Exception ex){
            return false;
        }
    }

    public static UUID gameKeyForPlayer(String playerName) {
        for(UUID key : GameRooms.keySet()){
            if (GameRooms.get(key).isPlayerHere(playerName)){
                return key;
            }
        }
        return null;
    }

    public static boolean isPlayerWinner(String playerName) {
        try {
            return GameRooms.get(gameKeyForPlayer(playerName)).isPlayerWinner(playerName);
        }
        catch (Exception ex){
            return false;
        }
    }
}
