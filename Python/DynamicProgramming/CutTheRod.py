import Common

# e.g. 1cm = $2
referenceLengthAndValues = [
	[1, 2],
	[2, 5],
	[3, 7],
	[4, 8]
]

targetLength = 5

matrix = []
solution = []
numOfRows = len(referenceLengthAndValues)
numOfColumns = targetLength + 1

def computeMatrix():
    for rowIndex in range(0, numOfRows, 1):
        row = []
        matrix.append(row)
        referenceLength = referenceLengthAndValues[rowIndex][0]
        referenceValue = referenceLengthAndValues[rowIndex][1]
        for columnIndex in range(0, numOfColumns, 1):
            currentTotalLength = columnIndex
            if currentTotalLength == 0:
                row.append(0)
            else:
                # LHS candidate for max (previous row value)
                option1 = 0
                if rowIndex > 0:
                    option1 = matrix[rowIndex - 1][columnIndex]
                
                # RHS candidate for max
                option2 = 0
                if currentTotalLength >= referenceLength:
                    option2 = referenceValue
                # Add the value of remaining maximized length (reuse the sub-problem)
                previousColumnIndex = columnIndex - referenceLength
                if previousColumnIndex > 0:
                    option2 = option2 + matrix[rowIndex][previousColumnIndex]
                # Take max
                maxValue = max(option1, option2)
                row.append(maxValue)

def backtrack():
    row = numOfRows - 1
    column = numOfColumns - 1
    while column >= 0 and row >= 0:
        currentRowValue = matrix[row][column]
        previousRowValue = matrix[row - 1][column] if row - 1 >= 0 else 0
        previousColumnValue = matrix[row][column - 1] if column >= 0 else 0
        if currentRowValue == previousRowValue:
            row = row - 1    
        else:
            # pick the current row in solution
            solution.append(referenceLengthAndValues[row])
            # subtract column by current length
            column = column - referenceLengthAndValues[row][0]
    
print("Target length = {0}".format(targetLength))
Common.printMatrix(referenceLengthAndValues, label="Input matrix", r="first column length", c="second column price")
computeMatrix()
Common.printMatrix(matrix, label="Generated matrix", r="input length and price", c="target length", symbol="$")
backtrack()
Common.printMatrix(solution, label="Solution matrix", r="first column length", c="second column price")