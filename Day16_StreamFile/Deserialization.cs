Stream stream = new FileStream("MyObject.dat", FileMode.Open);
BinaryFormatter formatter = new BinaryFormatter();
MyObject obj = (MyObject)formatter.Deserialize(stream);
stream.Close();