# e.g. 5g = $7
weightAndValues = [
	[1, 1],
	[3, 4],
	[4, 5],
	[5, 7]
]

targetWeight = 7
matrix = []
for rowIndex in range(0, len(weightAndValues), 1):
	matrix.append([])
	row = matrix[rowIndex]
	currentWeight = weightAndValues[rowIndex][0];
	currentValue = weightAndValues[rowIndex][1];
	for columnIndex in range(0, targetWeight + 1, 1):
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

for rowIndex in range(0, len(weightAndValues), 1):
	print(matrix[rowIndex])
	
#for key, value in weightAndValues.items():
#	print("key = %d, value = %d" %(key, value))