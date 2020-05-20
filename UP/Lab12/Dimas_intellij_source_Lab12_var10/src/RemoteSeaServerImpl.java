
import javafx.util.Pair;

import java.rmi.*;
import java.rmi.registry.*;
import java.rmi.server.*;

public class RemoteSeaServerImpl implements RemoteSeaServer {

    public static final String BINDING_NAME = "sample/HelloService";

    public Object sayHello(String name) {
        String string = "Hello, " + name + "!";
        try {
            System.out.println(name + " from " + UnicastRemoteObject.getClientHost());
        } catch (ServerNotActiveException e) {
            System.out.println(e.getMessage());
        }
        if ("Killer".equals(name)) {
            System.out.println("Shutting down...");
            System.exit(1);
        }
        return string;
    }

    @Override
    public boolean battleFound(String playerName) throws RemoteException {
        if (BattleManager.gameKeyForPlayer(playerName) != null){
            return true;
        }
        else {
            BattleManager.addPlayerToQueue(playerName);
            return false;
        }
    }

    @Override
    public boolean myGameFinished(String user) throws RemoteException {
        return BattleManager.gameFinishedForUser(user);
    }

    @Override
    public String getCurrentStepData(String playerName) throws RemoteException {
        return BattleManager.getBattleData(playerName);
    }

    @Override
    public boolean isNowCurrentStepOf(String playerName) throws RemoteException {
        return BattleManager.isNowCurrentStepOf(playerName);
    }

    @Override
    public void shoot(Pair<Integer, Integer> point, String shootPlayerName) throws RemoteException {
        BattleManager.shoot(point, shootPlayerName);
    }

    @Override
    public boolean winner(String playerName) throws RemoteException {
        return BattleManager.isPlayerWinner(playerName);
    }

    public static void main(String... args) throws Exception {
        System.out.print("Starting registry...");
        final Registry registry = LocateRegistry.createRegistry(2099);
        System.out.println(" OK");

        final RemoteSeaServer service = new RemoteSeaServerImpl();
        Remote stub = UnicastRemoteObject.exportObject(service, 0);

        System.out.print("Binding service...");
        registry.bind(BINDING_NAME, stub);
        System.out.println(" OK");

        while (true) {
            Thread.sleep(Integer.MAX_VALUE);
        }
    }
}