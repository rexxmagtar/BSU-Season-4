
import javafx.util.Pair;

import javax.swing.plaf.synth.SynthUI;
import java.rmi.registry.*;
import java.util.Scanner;

import static java.lang.Thread.sleep;

public class SeaClient {

    private static Scanner in = new Scanner(System.in);

    private static String registrateUser(RemoteSeaServer service){
        System.out.print("Enter your new battle name: ");
        return in.nextLine();
    }

    public static void main(String... args) throws Exception {
        Registry registry = LocateRegistry.getRegistry("localhost", 2099);
        RemoteSeaServer service = (RemoteSeaServer) registry.lookup("sample/HelloService");

        System.out.println("Logging....");
        String myplayerName = registrateUser(service);
        System.out.println("Logged OK!");

        do{
            if (userWantsToSearchBattle()){
                System.out.print("Searching a battle for you. Please, wait...");
                while(!service.battleFound(myplayerName)){
                    sleep(3);
                }
                System.out.println("\nA battle found!");
                while(!service.myGameFinished(myplayerName)){
                    System.out.println("____________________________________________________");
                    String currentStepData = service.getCurrentStepData(myplayerName);
                    System.out.println(currentStepData);
                    System.out.println("____________________________________________________");
                    if (service.isNowCurrentStepOf(myplayerName)){
                        System.out.println("This is your turn now!");
                        Pair<Integer, Integer> shotCoord = getShotCoordFromUser();
                        service.shoot(shotCoord, myplayerName);
                    }
                    else {
                        System.out.println("Please, wait: the other player is shooting!");
                        while(!service.isNowCurrentStepOf(myplayerName) && !service.myGameFinished(myplayerName)) {
                            sleep(3);
                        }
                        System.out.println("The other player has made a shot!!!!!!!!!!!!!!!!!!!");
                    }
                    System.out.println("\n`````````````````````````````````````````````````````````````````````````````\n");
                }
                if (service.winner(myplayerName)){
                    System.out.println("Congratulations: you won!");
                }
                else {
                    System.out.println("Unfortunately you lost the game, but don't worry: you train and you'll soon win a game!");
                }
            }
        } while(userWantsToContinue());
    }

    private static Pair<Integer, Integer> getShotCoordFromUser() {
        System.out.print("x: ");
        int x = Integer.parseInt(in.nextLine());
        System.out.print("y: ");
        int y = Integer.parseInt(in.nextLine());
        System.out.println("Accepted!");

        return new Pair<>(x, y);
    }

    private static boolean userWantsToSearchBattle() {
        System.out.print("Would you like to find a battle? Y/n: ");
        return in.nextLine().equals("Y");
    }

    private static boolean userWantsToContinue() {
        System.out.print("\n==========================\nWould you like to continue? Y/n: ");
        return in.nextLine().equals("Y");
    }

}