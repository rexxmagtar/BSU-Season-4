
import javafx.util.Pair;

import java.rmi.*;

public interface RemoteGameServer extends Remote {
    

    boolean GameFound(String forUser) throws RemoteException;

    boolean myGameFinished(String forUser) throws RemoteException;

    String getCurrentStepData(String playerName) throws RemoteException;

    boolean isNowCurrentStepOf(String playerName) throws RemoteException;

    void placeSymbol(Pair<Integer, Integer> point, String senderName) throws RemoteException;

    boolean winner(String userName) throws RemoteException;
}