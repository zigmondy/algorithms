def printMatrix(m, label="", r="", c="", symbol=""):
    print("-----------------------------------")
    print("{0} - {1} x {2} - {3} x {4}:".format(label, len(m), len(m[0]), r, c))
    print("-----------------------------------")
    for rowIndex in range(len(m)):
        buffer = ""
        for columnIndex in range(len(m[0])):
	        buffer = buffer + "{0}{1} ".format(symbol, m[rowIndex][columnIndex])
        buffer = "[" + buffer.rstrip() + "]"
        print(buffer)

def printVector(v, label=""):
    print("-----------------------------------")    
    print("{0} - {1}".format(label, len(v)))
    print("-----------------------------------")    
    print(v)