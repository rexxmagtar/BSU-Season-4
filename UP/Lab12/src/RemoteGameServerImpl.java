
import javafx.util.Pair;

import java.rmi.*;
import java.rmi.registry.*;
import java.rmi.server.*;

public class RemoteGameServerImpl implements RemoteGameServer {

    public static final String BINDING_NAME = "sample/HelloService";

    @Override
    public boolean GameFound(String playerName) throws RemoteException {
        if (GameManager.gameKeyForPlayer(playerName) != null){
            return true;
        }
        else {
            GameManager.addPlayerToQueue(playerName);
            return false;
        }
    }

    @Override
    public boolean myGameFinished(String user) throws RemoteException {
        return GameManager.gameFinishedForUser(user);
    }

    @Override
    public String getCurrentStepData(String playerName) throws RemoteException {
        return GameManager.getGameData(playerName);
    }

    @Override
    public boolean isNowCurrentStepOf(String playerName) throws RemoteException {
        return GameManager.isNowCurrentStepOf(playerName);
    }

    @Override
    public void placeSymbol(Pair<Integer, Integer> point, String senderName) throws RemoteException {
        GameManager.shoot(point, senderName);
    }

    @Override
    public boolean winner(String playerName) throws RemoteException {
        return GameManager.isPlayerWinner(playerName);
    }

    @Override
    public boolean isDraw(String playerName) throws RemoteException {
return GameManager.isDraw(playerName);
    }

    public static void main(String... args) throws Exception {
        System.out.print("Starting registry...");
        final Registry registry = LocateRegistry.createRegistry(2099);
        System.out.println(" OK");

        final RemoteGameServer service = new RemoteGameServerImpl();
        Remote stub = UnicastRemoteObject.exportObject(service, 0);

        System.out.print("Binding service...");
        registry.bind(BINDING_NAME, stub);
        System.out.println(" OK");

        while (true) {
            Thread.sleep(Integer.MAX_VALUE);
        }
    }
}