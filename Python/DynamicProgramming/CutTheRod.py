# e.g. 1cm = $2
referenceLengthAndValues = [
	[1, 2],
	[2, 5],
	[3, 7],
	[4, 8]
]

targetLength = 5

matrix = []
numOfRows = len(referenceLengthAndValues)
numOfColumns = targetLength + 1

def CreateMatrix():
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
                option1 = matrix[rowIndex - 1][columnIndex] if rowIndex > 0 else 0
                # RHS candidate for max
                option2 = 0;
                if currentTotalLength >= referenceLength:
                    option2 = referenceValue
                # Add the value of remaining maximized length (reuse the sub-problem)
                previousColumnIndex = columnIndex - referenceLength
                if previousColumnIndex > 0:
                    option2 = option2 + matrix[rowIndex][previousColumnIndex]
                # Take max
                maxValue = option2 if option2 > option1 else option1
                row.append(maxValue)

def PrintMatrix():
    print("Generated Matrix:")
    for rowIndex in range(0, numOfRows, 1):
        buffer = ""
        for columnIndex in range(0, numOfColumns, 1):
	        buffer = buffer + "${} ".format(matrix[rowIndex][columnIndex])
        buffer = "[" + buffer.rstrip() + "]"
        print(buffer)

CreateMatrix()
PrintMatrix()