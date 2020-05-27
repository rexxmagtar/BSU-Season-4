import javafx.util.Pair;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Random;
import java.util.UUID;


public class GameRoom {
    private UUID GameId;
    private int numOfSteps = 0;
    private String firstPlayer, secondPlayer;
    private String currentPlayerName;
    private GameRoomStatus currentStatus;
    private GameMap GameMap;

    public boolean isDraw=false;

    public boolean isPlayerHere(String playerName){
        return firstPlayer.equals(playerName) || secondPlayer.equals(playerName);
    }

    public void doShoot(Pair<Integer, Integer> point, String supposedShootPlayerName){
        if (supposedShootPlayerName.equals(currentPlayerName)){
            if (this.GameMap.Trymark(point)){
                setNextPlayerTurn();
            }
        }
    }

    private void setNextPlayerTurn() {
        currentPlayerName = (currentPlayerName.equals(firstPlayer)) ? secondPlayer : firstPlayer;
    }

    public GameRoom(String firstPlayer, String secondPlayer){
        this.firstPlayer = firstPlayer;
        this.secondPlayer = secondPlayer;
        //no matter who starts
        this.currentPlayerName = firstPlayer;

        GameId = UUID.randomUUID();
        GameMap = new GameMap();
        currentStatus = GameRoomStatus.Playing;
    }

    public boolean isNowCurrentStepOf(String playerName){
        return currentPlayerName.equals(playerName);
    }

    public  UUID getId(){
        return this.GameId;
    }

    public boolean gameFinished(){
        if (isPlayerWinner(firstPlayer) ||isPlayerWinner(secondPlayer)  ){
            this.currentStatus = GameRoomStatus.Finished;

            return true;
        }
        else{

            if(GameMap.checkIfdraw()){
               return true;
            }
            return false;
        }
    }

    public boolean isPlayerWinner(String playerName){
        return ((firstPlayer.equals(playerName) && GameMap.winnerId() == 1) ||
                (secondPlayer.equals(playerName) && GameMap.winnerId() == 2));
    }

    public String getGameData(String playerName) {
        return "Number of steps:\t" + numOfSteps + "\n" +
                "MAP:\n" + GameMap.getBeautifulMap() + "\n" +
                "Your symbol:\n" + GameMap.getPlayersSymbol(playerName) + "\n";
    }

    class GameMap{
        protected final static char EMPTY = '.', CROSS = 'X', ZERO = 'O';
        private ArrayList<Pair<Integer, Integer>> firstPlayerShips, secondPlayerShips;
        char Player1Symbol = CROSS;
        char Player2Symbol = ZERO;
        protected final int MAP_SIZE = 3;
        private char[][] map;

        protected GameMap(){
            map = new char[MAP_SIZE][MAP_SIZE];

            for(int i = 0; i < MAP_SIZE; i++){
                Arrays.fill(map[i], EMPTY);
            }

        }

        protected int winnerId(){
            if (HasRowOfThree(Player1Symbol)){
                return 1;
            }
            else if (HasRowOfThree(Player2Symbol)){
                return 2;
            }
            else{
                if(checkIfdraw()){
                    System.out.println("Draw detected");
                    isDraw=true;
                    return 0;
                }
                return -1;
            }
        }

        public boolean checkIfdraw(){

            for (int i = 0; i < map.length; i++) {

                for (int j = 0; j < map.length; j++) {

                    if(map[i][j] == EMPTY){
return false;
                    }
                }
            }

            return true;
        }

        public boolean HasRowOfThree(char symbol){

            for (int i = 0; i < map.length; i++) {

                boolean hasRow = false;

                for (int j = 0; j < map.length; j++) {
                    if(map[i][j] !=symbol ){
                        break;
                    }

                    if(j==map.length-1){
                        return true;
                    }
                }

            }


            for (int i = 0; i < map.length; i++) {

                boolean hasRow = false;

                for (int j = 0; j < map.length; j++) {
                    if(map[j][i] !=symbol ){
                        break;
                    }

                    if(j==map.length-1){
                        return true;
                    }
                }

            }

            for (int i = 0; i < map.length; i++) {

                boolean hasRow = false;


                    if(map[i][i] !=symbol ){
                        break;
                    }

                    if(i==map.length-1){
                        return true;
                    }


            }

            for (int i = 0; i < map.length; i++) {

                boolean hasRow = false;


                if(map[i][map.length-1-i] !=symbol ){
                    break;
                }

                if(i==map.length-1){
                    return true;
                }


            }

return false;

        }


        protected boolean Trymark(Pair<Integer, Integer> mark){

            if(map[mark.getKey()][mark.getValue()]!=EMPTY){

                System.out.println("This cell is already occupied \n");

                return false;

            }
                map[mark.getKey()][mark.getValue()] = getPlayersSymbol(currentPlayerName).charAt(0);

                ++numOfSteps;
                return true;

        }


        protected String getBeautifulMap(){
            StringBuilder strMap = new StringBuilder();
            strMap.append("\t   ");
            for(int i = 0; i < MAP_SIZE; i++){
                strMap.append((i+1) + "  ");
            }
            strMap.append("\n");
            for(int i = 0; i < MAP_SIZE; i++){
                strMap.append("\t" + (i+1) + "  ");
                for(int j = 0; j < MAP_SIZE; j++){
                    strMap.append(GameMap.map[i][j] + "  ");
                }
                strMap.append("\n");
            }
            return strMap.toString();
        }

        public String getPlayersSymbol(String playerName) {
            if (playerName.equals(firstPlayer)){
                return Character.toString( Player1Symbol) ;
            }
            else if (playerName.equals(secondPlayer)){
                return Character.toString( Player2Symbol) ;
            }
            else{
                return "";
            }
        }
    }
}

enum GameRoomStatus {
    Playing,
    Finished
}
