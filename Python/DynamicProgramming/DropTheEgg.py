import Common

eggs = 2
floors = 6

m = [[0 for floor in range(floors + 1)] for egg in range(eggs + 1)]

def computeMatrix():
    for egg in range(1, eggs + 1, 1):
        for floor in range(1, floors + 1, 1):
            if egg == 1:
                m[egg][floor] = floor
            elif egg > floor:
                # Copy from above
                m[egg][floor] = m[egg - 1][floor]
            else:
                print("==")
                for currentFloor in range(1, floor, 1):
                    print("eggs={0}, currentFloor={1}, cost=1 + max(m[{2}][{3}], m[{4}][{5}])".format(egg, currentFloor, egg - 1, currentFloor - 1, egg, floor - currentFloor))
                    cost = 1 + max(m[egg - 1][currentFloor - 1], m[egg][floor - currentFloor])
                    if m[egg][floor] == 0:
                        m[egg][floor] = cost
                    else:     
                        m[egg][floor] = min(cost, m[egg][floor]) 
                
computeMatrix()
Common.printMatrix(m, label="Optimal Cost", r="Eggs", c="Floors")
print("Number of attempts = {0}".format(m[eggs][floors]))