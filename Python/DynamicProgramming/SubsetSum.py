import Common

referenceNumbers = [2, 3, 7, 8, 10]
desiredSum = 11

rows = len(referenceNumbers)
columns = desiredSum
m = [[False for _ in range(columns)] for _ in range(rows)]
backtrack = []

def computeMatrix():
    for row in range(0, rows):
        for column in range(0, columns):
            currentRowNumber = referenceNumbers[row]
            currentColumnSum = column + 1
            if currentColumnSum < currentRowNumber:
                if row == 0:
                    m[row][column] = False
                else:
                    # Copy previous row value
                    m[row][column] = m[row - 1][column]
            elif currentColumnSum == currentRowNumber:
                m[row][column] = True
            else:
                m[row][column] = m[row - 1][column] or m[row - 1][column - currentRowNumber]

def backTrack():
    row = rows - 1
    column = columns - 1
    while column >= 0 and row >= 0:
        currentRowValue = m[row][column]
        previousRowValue = m[row - 1][column] if row - 1 >= 0 else False
        if currentRowValue == previousRowValue:
            row = row - 1
        else:
            backtrack.insert(0, referenceNumbers[row])
            column = column - referenceNumbers[row]

Common.printVector(referenceNumbers, label="Vector referenceNumbers")
computeMatrix()
Common.printMatrix(m, label="Matrix", r="Row Size", c="Column Size")
backTrack()
Common.printVector(backtrack, label="Backtrack numbers")