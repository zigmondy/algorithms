def printMatrix(m, label="Matrix - {0} x {1} - {2} x {3}:", r="", c="", symbol=""):
    print("-----------------------------------")
    print(label.format(len(m), len(m[0]), r, c))
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