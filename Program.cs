﻿

public enum KELURAHAN
{
    BATUNANGGAL,
    KUJANGSARI,
    MENGGER,
    WATES,
    CIJAURA,
    JATISARI,
    MARGASARI,
    SEKEJATI,
    KEBONWARU,
    MALEER,
    SAMOJA,
};

public enum DOORSTATES
{
    TERKUNCI,
    TERBUKA,
}

public enum DOORTRIGGERS
{
    KunciPintu,
    BukaPintu,
}

internal class Program
{
    private static void Main(string[] args)
    {
        int kPos = KodePos.getKodePos(KELURAHAN.JATISARI);
        Console.WriteLine("Kode pos Jatisari adalah: " + kPos);
        DoorMachine pintu = new DoorMachine();
        Console.WriteLine("Pintu saat ini sedang " + pintu.current);
        pintu.Action(DOORTRIGGERS.BukaPintu);
        pintu.Action(DOORTRIGGERS.KunciPintu);
        pintu.Action(DOORTRIGGERS.KunciPintu);
    }
}

public class KodePos
{
    public static int getKodePos(KELURAHAN kelurahan)
    {
        int[] kodePos = { 40266, 40287, 40267, 40256, 40287, 40286, 40286, 40286, 40272, 40274, 40273 };
        return kodePos[(int)kelurahan];
    }
}

public class Shifts
{
    public DOORSTATES initDoorState;
    public DOORSTATES finalDoorState;
    public DOORTRIGGERS trigger;

    public Shifts(DOORSTATES initDoorState, DOORSTATES finalDoorState, DOORTRIGGERS trigger)
    {
        this.initDoorState = initDoorState;
        this.finalDoorState = finalDoorState;
        this.trigger = trigger;
    }

}

public class DoorMachine
{
    public DOORSTATES current;

    public DoorMachine()
    {
        current = DOORSTATES.TERKUNCI;
    }

    Shifts[] Transitions =
    {
        new Shifts(DOORSTATES.TERBUKA, DOORSTATES.TERKUNCI, DOORTRIGGERS.KunciPintu),
        new Shifts(DOORSTATES.TERKUNCI, DOORSTATES.TERBUKA, DOORTRIGGERS.BukaPintu),
    };

    public DOORSTATES NextState(DOORSTATES initState, DOORTRIGGERS trigger)
    {
        DOORSTATES finalState = initState;
        for (int i = 0; i < Transitions.Length; i++)
        {
            Shifts change = Transitions[i];
            if (initState == change.initDoorState && trigger == change.trigger)
            {
                finalState = change.finalDoorState;
            }
        }

        return finalState;
    }

    public void Action(DOORTRIGGERS trigger)
    {
        current = NextState(current, trigger);

        if (trigger == DOORTRIGGERS.KunciPintu)
        {
            Console.WriteLine("Mengunci...");
        }
        else if (trigger == DOORTRIGGERS.BukaPintu)
        {
            Console.WriteLine("Membuka kunci...");
        }

        if (current == DOORSTATES.TERKUNCI)
        {
            Console.WriteLine("Pintu terkunci");
        }
        else if (current == DOORSTATES.TERBUKA)
        {
            Console.WriteLine("Pintu terbuka");
        }
    }
}