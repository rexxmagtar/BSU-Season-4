
import javafx.util.Pair;

import java.rmi.*;

public interface RemoteSeaServer extends Remote {

    Object sayHello(String name) throws RemoteException;

    boolean battleFound(String forUser) throws RemoteException;

    boolean myGameFinished(String forUser) throws RemoteException;

    String getCurrentStepData(String playerName) throws RemoteException;

    boolean isNowCurrentStepOf(String playerName) throws RemoteException;

    void shoot(Pair<Integer, Integer> point, String shootPlayerName) throws RemoteException;

    boolean winner(String userName) throws RemoteException;
}