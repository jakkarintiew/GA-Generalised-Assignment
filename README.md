# GA-Generalised-Assignment

Genetic Algorithm Generalised Assignment Problem (GAP) Solver, C# Implementation.

## Generalised Assignment Problem
Given m workers and n xobs with:

> _p<sub>ij</sub>_ = cost of job<sub>_j_</sub> assigned to worker<sub>_i_</sub>, 
>
> _m >= n_,
>
> _i = 0, ..., m_,
>
> _j = 0, ..., n_
<!-- > 
> _w_<sub>ij</sub> = weight of job<sub>_j_</sub> assigned to worker<sub>_i_</sub>,
>
> _C_<sub>i</sub>  = capacity of worker<sub>_i_</sub> -->


### Chromosone (solution) representation
A array of size _n_, to represent all jobs:

> [ _x<sub>0</sub>_, _x<sub>1</sub>_, _x<sub>2</sub>_, ..., _x<sub>n</sub>_ ], where
>
> _x<sub>n</sub> = 0, 1, 2, ..., m_

i.e. _x<sub>0</sub> = 3_ means job<sub>_0_</sub> is assigned to worker<sub>_3_</sub>


### Functions
#### GetRandomAssignment
#### FitnessFunction
#### ChooseParent
#### Crossover
#### Mutate

### Sample Output
```
====== Assignment Problem Description ======
Number of workers: 4
Number of jobs: 6

-------------------------------
Current Generation: 999
Current Best Fitness: 106
Population Size: 100
Mutation Rate: 0.05
-------------------------------

====== Generation: 1000 ======
Assignment: [ 0, 3, 2, 0, 0, 1 ]
Best Fitness Score (cost): 106
Best Population Count: 100
RunTime: 00:00:00.264
=============================
```

## ToDo's
1. Add flow/weight matrix
2. Add capacity to each worker

      > _w_<sub>ij</sub> = weight of job<sub>_j_</sub> assigned to worker<sub>_i_</sub>,
      >
      > _C_<sub>i</sub>  = capacity of worker<sub>_i_</sub>

3. Add constraint checker
