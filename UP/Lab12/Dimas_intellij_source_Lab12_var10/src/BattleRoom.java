import javafx.util.Pair;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Random;
import java.util.UUID;


public class BattleRoom {
    private UUID battleId;
    private int numOfSteps = 0;
    private String firstPlayer, secondPlayer;
    private String currentPlayerName;
    private BattleRoomStatus currentStatus;
    private BattleMap battleMap;

    public boolean isPlayerHere(String playerName){
        return firstPlayer.equals(playerName) || secondPlayer.equals(playerName);
    }

    public void doShoot(Pair<Integer, Integer> point, String supposedShootPlayerName){
        if (supposedShootPlayerName.equals(currentPlayerName)){
            if (!this.battleMap.shotHitSomeone(point)){
                setNextPlayerTurn();
            }
        }
    }

    private void setNextPlayerTurn() {
        currentPlayerName = (currentPlayerName.equals(firstPlayer)) ? secondPlayer : firstPlayer;
    }

    public BattleRoom(String firstPlayer, String secondPlayer){
        this.firstPlayer = firstPlayer;
        this.secondPlayer = secondPlayer;
        //no matter who starts
        this.currentPlayerName = firstPlayer;

        battleId = UUID.randomUUID();
        battleMap = new BattleMap();
        currentStatus = BattleRoomStatus.Playing;
    }

    public boolean isNowCurrentStepOf(String playerName){
        return currentPlayerName.equals(playerName);
    }

    public  UUID getId(){
        return this.battleId;
    }

    public boolean gameFinished(){
        if (battleMap.somePlayerLostAllShips()){
            this.currentStatus = BattleRoomStatus.Finished;
            return true;
        }
        else{
            return false;
        }
    }

    public boolean isPlayerWinner(String playerName){
        return ((firstPlayer.equals(playerName) && battleMap.winnerId() == 1) ||
                (secondPlayer.equals(playerName) && battleMap.winnerId() == 2));

    }

    public String getBattleData(String playerName) {
        return "Number of steps:\t" + numOfSteps + "\n" +
                "MAP:\n" + battleMap.getBeautifulMap() + "\n" +
                "Your ships:\n" + battleMap.getMapOfShips(playerName) + "\n";
    }


    class BattleMap{
        protected final static char EMPTY = '.', CLOSED = '+', BOOMED = 'K';
        private ArrayList<Pair<Integer, Integer>> firstPlayerShips, secondPlayerShips;
        protected final int MAP_SIZE = 10;
        public final int NUM_OF_SHIPS = 10;
        private char[][] map;
        private Random rand = new Random();

        protected BattleMap(){
            map = new char[MAP_SIZE][MAP_SIZE];

            for(int i = 0; i < MAP_SIZE; i++){
                Arrays.fill(map[i], EMPTY);
            }

            initializePlayerShips();
        }

        protected int winnerId(){
            if (firstPlayerShips.size() == 0){
                return 2;
            }
            else if (secondPlayerShips.size() == 0){
                return 1;
            }
            else{
                return -1;
            }
        }

        protected boolean somePlayerLostAllShips(){
            return firstPlayerShips.size() == 0 || secondPlayerShips.size() == 0;
        }

        private void initializePlayerShips() {
            boolean[][] forbidden_map = new boolean[MAP_SIZE][MAP_SIZE];
            for(int i = 0; i < MAP_SIZE; i++){
                Arrays.fill(forbidden_map[i], false);
            }
            firstPlayerShips = new ArrayList<>();
            secondPlayerShips = new ArrayList<>();
            
            for(int i = 0; i < NUM_OF_SHIPS; i++){
                Pair<Integer, Integer> first, second;
                first = getRandomCoordinate(forbidden_map);
                forbidden_map[first.getKey()][first.getValue()] = true;
                second = getRandomCoordinate(forbidden_map);
                forbidden_map[second.getKey()][second.getValue()] = true;
                firstPlayerShips.add(getRandomCoordinate(forbidden_map));
                secondPlayerShips.add(getRandomCoordinate(forbidden_map));
            }

        }

        private Pair<Integer, Integer> getRandomCoordinate(boolean[][] forbidden_map) {
            int x, y;
            do{
                x = rand.nextInt(MAP_SIZE);
                y = rand.nextInt(MAP_SIZE);
            } while(forbidden_map[x][y]);

            return new Pair<>(x, y);
        }


        protected boolean shotHitSomeone(Pair<Integer, Integer> shot){
            if (firstPlayerShips.contains(shot)) {
                map[shot.getKey()][shot.getValue()] = BOOMED;
                firstPlayerShips.remove(shot);
                return true;
            }
            else if (secondPlayerShips.contains(shot)){
                map[shot.getKey()][shot.getValue()] = BOOMED;
                secondPlayerShips.remove(shot);
                return true;
            }
            else{
                map[shot.getKey()][shot.getValue()] = CLOSED;
                ++numOfSteps;
                return false;
            }
        }


        protected String getBeautifulMap(){
            StringBuilder strMap = new StringBuilder();
            strMap.append("\t   ");
            for(int i = 0; i < MAP_SIZE; i++){
                strMap.append(i + "  ");
            }
            strMap.append("\n");
            for(int i = 0; i < MAP_SIZE; i++){
                strMap.append("\t" + i + "  ");
                for(int j = 0; j < MAP_SIZE; j++){
                    strMap.append(battleMap.map[i][j] + "  ");
                }
                strMap.append("\n");
            }
            return strMap.toString();
        }

        public String getMapOfShips(String playerName) {
            if (playerName.equals(firstPlayer)){
                return firstPlayerShips.toString();
            }
            else if (playerName.equals(secondPlayer)){
                return secondPlayerShips.toString();
            }
            else{
                return "";
            }
        }
    }
}

enum BattleRoomStatus {
    Playing,
    Finished
}
