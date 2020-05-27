
import javafx.util.Pair;

import java.rmi.registry.*;
import java.util.Scanner;

import static java.lang.Thread.sleep;

public class GameClient {

    private static Scanner in = new Scanner(System.in);

    private static String registrateUser(RemoteGameServer service) {
        System.out.print("Enter your new Game name: ");
        return in.nextLine();
    }

    public static void main(String... args) throws Exception {
        Registry registry = LocateRegistry.getRegistry("localhost", 2099);
        RemoteGameServer service  = (RemoteGameServer) registry.lookup("sample/HelloService");

        System.out.println("Logging....");
        String myplayerName = registrateUser(service);
        System.out.println("Logged OK!");


        if (userWantsToSearchGame()) {
            System.out.print("Searching a Game for you. Please, wait...");
            while (!service.GameFound(myplayerName)) {
                sleep(3);
            }
            System.out.println("\nA Game found!");
            while (!service.myGameFinished(myplayerName)) {
                System.out.print("\033[H\033[2J");
                System.out.flush();
                System.out.println("____________________________________________________");
                String currentStepData = service.getCurrentStepData(myplayerName);
                System.out.println(currentStepData);
                System.out.println("____________________________________________________");
                if (service.isNowCurrentStepOf(myplayerName)) {
                    System.out.println("This is your turn now!");
                    Pair<Integer, Integer> markCoord = getmarkCoordFromUser();
                    service.placeSymbol(markCoord, myplayerName);
                } else {
                    System.out.println("Wating for opponent to make move");
                    while (!service.isNowCurrentStepOf(myplayerName) && !service.myGameFinished(myplayerName)) {
                        sleep(3);
                    }

                }
                System.out.println("\n`````````````````````````````````````````````````````````````````````````````\n");
            }

            System.out.print("\033[H\033[2J");
            System.out.flush();
            String currentStepData = service.getCurrentStepData(myplayerName);
            System.out.println(currentStepData);
            System.out.println("____________________________________________________");
            if (service.winner(myplayerName)) {
                System.out.println("Congratulations: you won!");
            } else {
                if (service.isDraw(myplayerName)) {
                    System.out.println("Draw! nice game");

                } else {
                    System.out.println("Unfortunately you lost the game, but don't worry: you train and you'll soon win a game!");
                }
            }
        }

    }

    private static Pair<Integer, Integer> getmarkCoordFromUser() {
        System.out.print("x: ");
        int x = Integer.parseInt(in.nextLine());
        System.out.print("y: ");
        int y = Integer.parseInt(in.nextLine());
        System.out.println("Accepted!");

        return new Pair<>(x - 1, y - 1);
    }

    private static boolean userWantsToSearchGame() {
        System.out.print("Would you like to find a Game? y/n: ");
        return in.nextLine().equals("y");
    }


}