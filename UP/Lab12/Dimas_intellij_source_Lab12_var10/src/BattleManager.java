import javafx.util.Pair;

import java.util.HashMap;
import java.util.UUID;

public class BattleManager {
    private static String waitingPlayer = null;
    private static HashMap<UUID, BattleRoom> battleRooms = new HashMap<>();

    public static void addPlayerToQueue(String playerName) {
        if (waitingPlayer != null && !waitingPlayer.equals(playerName)) {
            BattleRoom room = new BattleRoom(waitingPlayer, playerName);
            battleRooms.put(room.getId(), room);
            waitingPlayer = null;
        } else {
            waitingPlayer = playerName;
        }
    }


    public static void shoot(Pair<Integer, Integer> point, String playerName){
        try {
            battleRooms.get(gameKeyForPlayer(playerName)).doShoot(point, playerName);
        }
        catch (Exception ex){
            //return false;
        }
    }

    public static boolean isNowCurrentStepOf(String playerName){
        try {
            return battleRooms.get(gameKeyForPlayer(playerName)).isNowCurrentStepOf(playerName);
        }
        catch (Exception ex){
            return false;
        }
    }

    public static String getBattleData(String playerName){
        try {
            return battleRooms.get(gameKeyForPlayer(playerName)).getBattleData(playerName);
        }
        catch (Exception ex){
            return "There is no battle for you!";

        }
    }

    public static boolean gameFinishedForUser(String playerName){
        try {
            return battleRooms.get(gameKeyForPlayer(playerName)).gameFinished();
        }
        catch (Exception ex){
            return false;
        }
    }

    public static UUID gameKeyForPlayer(String playerName) {
        for(UUID key : battleRooms.keySet()){
            if (battleRooms.get(key).isPlayerHere(playerName)){
                return key;
            }
        }
        return null;
    }

    public static boolean isPlayerWinner(String playerName) {
        try {
            return battleRooms.get(gameKeyForPlayer(playerName)).isPlayerWinner(playerName);
        }
        catch (Exception ex){
            return false;
        }
    }
}
