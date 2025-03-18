// Create object
MyObject obj = new MyObject();

// Create a stream object that points to a file or memory stream that will be used to store the serialized object
Stream stream = new FileStream("MyObject.dat", FileMode.Create);

// Create a formatter object that will be used to serialize the object.
BinaryFormatter formatter = new BinaryFormatter();

//
formatter.Serialize(stream, obj);

stream.Close();