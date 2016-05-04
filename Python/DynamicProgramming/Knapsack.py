import Common

# e.g. 5g = $7
weightAndValues = [
	[1, 1],
	[3, 4],
	[4, 5],
	[5, 7]
]

targetWeight = 7

matrix = []
solution = []
numOfRows = len(weightAndValues)
numOfColumns = targetWeight + 1

def generateMatrix():
	for rowIndex in range(0, numOfRows, 1):
		matrix.append([])
		row = matrix[rowIndex]
		currentWeight = weightAndValues[rowIndex][0];
		currentValue = weightAndValues[rowIndex][1];
		for columnIndex in range(0, numOfColumns, 1):
			currentTotalWeight = columnIndex;
			if (currentTotalWeight == 0):
				row.append(0)
			elif (rowIndex == 0):
				row.append(currentValue)
			elif (currentTotalWeight < currentWeight):
				# Copy the value from row - 1, column
				row.append(matrix[rowIndex - 1][columnIndex])
			else:
				# Option 1 - sum of current value and excess weight value
				excessWeight = currentTotalWeight - currentWeight
				excessWeightValue = matrix[rowIndex - 1][excessWeight]
				option1 = currentValue + excessWeightValue;
				# Option 2 - Previous Value
				option2 = matrix[rowIndex - 1][columnIndex]
				# Max
				row.append(max(option1, option2))

def backtrack():
	rowIndex = numOfRows - 1
	columnIndex = numOfColumns - 1
	remainingWeight = targetWeight
	while rowIndex >= 0:
		valueAtCurrentSpot = matrix[rowIndex][columnIndex]
		valueAtRowAbove = 0 
		if rowIndex - 1 >= 0:
			valueAtRowAbove = matrix[rowIndex - 1][columnIndex]
		if valueAtCurrentSpot == valueAtRowAbove:
			rowIndex = rowIndex - 1 # value was copied from above
		else:
			solution.append([weightAndValues[rowIndex][0], weightAndValues[rowIndex][1]])
			remainingWeight = remainingWeight - weightAndValues[rowIndex][0]
			if remainingWeight == 0:
				break;
			rowIndex = rowIndex - 1
			columnIndex = columnIndex - weightAndValues[rowIndex][1]

# Print the input
Common.printMatrix(weightAndValues, label="Input matrix", r="first column length", c="second column price")

generateMatrix()
# Print the generated matrix
Common.printMatrix(matrix, label="Generated matrix", r="input length and price", c="target length", symbol="$")

# Now find the optimal weights
backtrack()
Common.printMatrix(solution, label="Solution matrix", r="first column length", c="second column price")