using System;
using System.Collections.Generic;
using System.IO;

public interface IOper {
}

public interface ICustomField<T> where T : IOper {
    void Reset();
    void Write(T op);
    void Read(T op);
}

namespace BinaryOperator {
    public interface ICustomFieldBinary : ICustomField<BinaryOperator> {
    }

    public struct StInt2 : ICustomFieldBinary {
        public float x;
        public float y;

        public StInt2(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public void Reset() {
            this.x = 0f;
            this.y = 0f;
        }

        public void Read(BinaryOperator oper) {
            this.x = oper.ReadSingle();
            this.y = oper.ReadSingle();
        }

        public void Write(BinaryOperator oper) {
            oper.Write(this.x);
            oper.Write(this.y);
        }
    }

    public class BinaryOperator : IDisposable, IOper {
        private readonly BinaryReader opReader;
        private readonly BinaryWriter opWriter;

        public BinaryOperator(string fileFullPath) {
            Stream stream = new FileStream(fileFullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            this.opReader = new BinaryReader(stream);
            this.opWriter = new BinaryWriter(stream);
        }
        public void Dispose() {
            this.opReader?.Dispose();
            this.opWriter?.Dispose();
        }

        #region Read One
        public bool ReadBoolean() { return this.opReader.ReadBoolean(); }
        public byte ReadByte() { return this.opReader.ReadByte(); }
        public sbyte ReadSByte() { return this.opReader.ReadSByte(); }
        public short ReadInt16() { return this.opReader.ReadInt16(); }
        public ushort ReadUInt16() { return this.opReader.ReadUInt16(); }
        public int ReadInt32() { return this.opReader.ReadInt32(); }
        public uint ReadUInt32() { return this.opReader.ReadUInt32(); }
        public long ReadInt64() { return this.opReader.ReadInt64(); }
        public ulong ReadUInt64() { return this.opReader.ReadUInt64(); }
        public float ReadSingle() { return this.opReader.ReadSingle(); }
        public string ReadString() { return this.opReader.ReadString(); }

        // 为了表格中帧同步浮点数
        public float ReadFloat() { return this.opReader.ReadUInt32() * 0.001f; }
        public T Read<T>() where T : ICustomFieldBinary, new() {
            T t = new T();
            t.Read(this);
            return t;
        }
        #endregion

        #region  Read Array 对于数组，先读出length,然后逐个读出
        public IList<bool> ReadBooleanArray() {
            int length = this.opReader.ReadInt32();
            bool[] list = new bool[length];
            for (int i = 0; i < length; ++i) {
                list[i] = this.opReader.ReadBoolean();
            }
            return list;
        }
        public IList<byte> ReadByteArray() {
            int length = this.opReader.ReadInt32();
            byte[] list = new byte[length];
            for (int i = 0; i < length; ++i) {
                list[i] = this.opReader.ReadByte();
            }
            return list;
        }
        public IList<sbyte> ReadSByteArray() {
            int length = this.opReader.ReadInt32();
            sbyte[] list = new sbyte[length];
            for (int i = 0; i < length; ++i) {
                list[i] = this.opReader.ReadSByte();
            }
            return list;
        }
        public IList<short> ReadInt16Array() {
            int length = this.opReader.ReadInt32();
            short[] list = new short[length];
            for (int i = 0; i < length; ++i) {
                list[i] = this.opReader.ReadInt16();
            }
            return list;
        }
        public IList<ushort> ReadUInt16Array() {
            int length = this.opReader.ReadInt32();
            ushort[] list = new ushort[length];
            for (int i = 0; i < length; ++i) {
                list[i] = this.opReader.ReadUInt16();
            }
            return list;
        }
        public IList<int> ReadInt32Array() {
            int length = this.opReader.ReadInt32();
            int[] list = new int[length];
            for (int i = 0; i < length; ++i) {
                list[i] = this.opReader.ReadInt32();
            }
            return list;
        }
        public IList<uint> ReadUInt32Array() {
            int length = this.opReader.ReadInt32();
            uint[] list = new uint[length];
            for (int i = 0; i < length; ++i) {
                list[i] = this.opReader.ReadUInt32();
            }
            return list;
        }
        public IList<long> ReadInt64Array() {
            int length = this.opReader.ReadInt32();
            long[] list = new long[length];
            for (int i = 0; i < length; ++i) {
                list[i] = this.opReader.ReadInt64();
            }
            return list;
        }
        public IList<ulong> ReadUInt64Array() {
            int length = this.opReader.ReadInt32();
            ulong[] list = new ulong[length];
            for (int i = 0; i < length; ++i) {
                list[i] = this.opReader.ReadUInt64();
            }
            return list;
        }
        public IList<float> ReadSingleArray() {
            int length = this.opReader.ReadInt32();
            float[] list = new float[length];
            for (int i = 0; i < length; ++i) {
                list[i] = this.opReader.ReadSingle();
            }
            return list;
        }

        public IList<float> ReadFloatArray() {
            int length = this.opReader.ReadInt32();
            float[] list = new float[length];
            for (int i = 0; i < length; ++i) {
                list[i] = ReadFloat();
            }
            return list;
        }
        public IList<T> ReadArray<T>() where T : ICustomFieldBinary, new() {
            int length = this.opReader.ReadInt32();
            T[] list = new T[length];
            for (int i = 0; i < length; ++i) {
                list[i] = this.Read<T>();
            }
            return list;
        }
        #endregion

        #region  Read Array2 读取二维数组
        public IList<IList<bool>> ReadBooleanArray2() {
            int length = this.opReader.ReadInt32();
            IList<IList<bool>> list = new List<IList<bool>>(length);
            for (int i = 0; i < length; ++i) {
                list.Add(this.ReadBooleanArray());
            }
            return list;
        }
        public IList<IList<byte>> ReadByteArray2() {
            int length = this.opReader.ReadInt32();
            IList<IList<byte>> list = new List<IList<byte>>(length);
            for (int i = 0; i < length; ++i) {
                list.Add(this.ReadByteArray());
            }
            return list;
        }
        public IList<IList<sbyte>> ReadSByteArray2() {
            int length = this.opReader.ReadInt32();
            IList<IList<sbyte>> list = new List<IList<sbyte>>(length);
            for (int i = 0; i < length; ++i) {
                list.Add(this.ReadSByteArray());
            }
            return list;
        }
        public IList<IList<short>> ReadInt16Array2() {
            int length = this.opReader.ReadInt32();
            IList<IList<short>> list = new List<IList<short>>(length);
            for (int i = 0; i < length; ++i) {
                list.Add(this.ReadInt16Array());
            }
            return list;
        }
        public IList<IList<ushort>> ReadUInt16Array2() {
            int length = this.opReader.ReadInt32();
            IList<IList<ushort>> list = new List<IList<ushort>>(length);
            for (int i = 0; i < length; ++i) {
                list.Add(this.ReadUInt16Array());
            }
            return list;
        }
        public IList<IList<int>> ReadInt32Array2() {
            int length = this.opReader.ReadInt32();
            IList<IList<int>> list = new List<IList<int>>(length);
            for (int i = 0; i < length; ++i) {
                list.Add(this.ReadInt32Array());
            }
            return list;
        }
        public IList<IList<uint>> ReadUInt32Array2() {
            int length = this.opReader.ReadInt32();
            IList<IList<uint>> list = new List<IList<uint>>(length);
            for (int i = 0; i < length; ++i) {
                list.Add(this.ReadUInt32Array());
            }
            return list;
        }
        public IList<IList<long>> ReadInt64Array2() {
            int length = this.opReader.ReadInt32();
            IList<IList<long>> list = new List<IList<long>>(length);
            for (int i = 0; i < length; ++i) {
                list.Add(this.ReadInt64Array());
            }
            return list;
        }
        public IList<IList<ulong>> ReadUInt64Array2() {
            int length = this.opReader.ReadInt32();
            IList<IList<ulong>> list = new List<IList<ulong>>(length);
            for (int i = 0; i < length; ++i) {
                list.Add(this.ReadUInt64Array());
            }
            return list;
        }
        public IList<IList<float>> ReadSingleArray2() {
            int length = this.opReader.ReadInt32();
            IList<IList<float>> list = new List<IList<float>>(length);
            for (int i = 0; i < length; ++i) {
                list.Add(this.ReadSingleArray());
            }
            return list;
        }
        public IList<IList<float>> ReadFloatArray2() {
            int length = this.opReader.ReadInt32();
            IList<IList<float>> list = new List<IList<float>>(length);
            for (int i = 0; i < length; ++i) {
                list.Add(this.ReadFloatArray());
            }
            return list;
        }
        public IList<IList<T>> ReadArray2<T>() where T : ICustomFieldBinary, new() {
            int length = this.opReader.ReadInt32();
            IList<IList<T>> list = new List<IList<T>>(length);
            for (int i = 0; i < length; ++i) {
                list.Add(this.ReadArray<T>());
            }
            return list;
        }
        #endregion

        #region Write One
        public void Write(bool value) { this.opWriter.Write(value); }
        public void Write(byte value) { this.opWriter.Write(value); }
        public void Write(sbyte value) { this.opWriter.Write(value); }
        public void Write(short value) { this.opWriter.Write(value); }
        public void Write(ushort value) { this.opWriter.Write(value); }
        public void Write(int value) { this.opWriter.Write(value); }
        public void Write(uint value) { this.opWriter.Write(value); }
        public void Write(long value) { this.opWriter.Write(value); }
        public void Write(ulong value) { this.opWriter.Write(value); }
        public void Write(float value) { this.opWriter.Write(value); }
        public void Write(string value) { this.opWriter.Write(value); }
        public void WriteFloat(float value) { this.opWriter.Write((int)value * 1000); }
        public void Write<T>(T value) where T : ICustomFieldBinary, new() { value.Write(this); }
        #endregion

        #region Write Array 对于数组，先写入length,然后逐个写入,null或者长度为0的也写入，为了和readArray对应，否则readArray中直接readLength会出现问题
        public void Write(IList<bool> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.opWriter.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<byte> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.opWriter.Write(v);
                }
            }
            else {
                // 为null写入长度0
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<sbyte> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.opWriter.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<short> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.opWriter.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<ushort> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.opWriter.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<int> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.opWriter.Write(v);
                }
            }
            else {
                // 为null写入长度0
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<uint> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.opWriter.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<long> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.opWriter.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<ulong> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.opWriter.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<float> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.opWriter.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void WriteFloat(IList<float> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.WriteFloat(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write<T>(IList<T> values) where T : ICustomFieldBinary, new() {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    v.Write(this);
                }
            }
        }
        #endregion

        #region 写入二维数组
        public void Write(IList<IList<bool>> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<IList<byte>> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.Write(v);
                }
            }
            else {
                // 为null写入长度0
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<IList<sbyte>> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<IList<short>> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<IList<ushort>> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<IList<int>> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.Write(v);
                }
            }
            else {
                // 为null写入长度0
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<IList<uint>> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<IList<long>> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<IList<ulong>> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void Write(IList<IList<float>> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.Write(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        public void WriteFloat(IList<IList<float>> values) {
            if (values != null) {
                this.opWriter.Write(values.Count);
                foreach (var v in values) {
                    this.WriteFloat(v);
                }
            }
            else {
                this.opWriter.Write(0);
            }
        }
        #endregion
    }
}
