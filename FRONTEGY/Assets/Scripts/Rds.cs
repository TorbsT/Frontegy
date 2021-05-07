using System.Collections;
using System.Collections.Generic;
using System;

public class Rds
{
    private Random seedGen;
    private Rd caard0;  // Generating cards for player 0
    private Rd caard1;  // Generating cards for player 1
    private Rd tiile;  // Generating tile teams
    public Rds()
    {
        seedGen = new Random();
        createRds();
    }
    public Rds(int seed)
    {
        seedGen = new Random(seed);
        createRds();
    }
    private void createRds()
    {
        caard0 = new Rd(seedGen.Next());
        caard1 = new Rd(seedGen.Next());
        tiile = new Rd(seedGen.Next());
    }

    public Rd getCaard0() { return caard0; }
    public Rd getCaard1() { return caard1; }
    public Rd getTiile() { return tiile; }
}
