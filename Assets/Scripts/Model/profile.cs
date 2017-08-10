// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

using global::System;
using global::FlatBuffers;

public struct PlayerProfileFlat : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static PlayerProfileFlat GetRootAsPlayerProfileFlat(ByteBuffer _bb) { return GetRootAsPlayerProfileFlat(_bb, new PlayerProfileFlat()); }
  public static PlayerProfileFlat GetRootAsPlayerProfileFlat(ByteBuffer _bb, PlayerProfileFlat obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public static bool PlayerProfileFlatBufferHasIdentifier(ByteBuffer _bb) { return Table.__has_identifier(_bb, "FPCS"); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public PlayerProfileFlat __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Gold { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Items { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
  public ArraySegment<byte>? GetItemsBytes() { return __p.__vector_as_arraysegment(6); }
  public CharacterModelFlat? Characters { get { int o = __p.__offset(8); return o != 0 ? (CharacterModelFlat?)(new CharacterModelFlat()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<PlayerProfileFlat> CreatePlayerProfileFlat(FlatBufferBuilder builder,
      int gold = 0,
      StringOffset itemsOffset = default(StringOffset),
      Offset<CharacterModelFlat> charactersOffset = default(Offset<CharacterModelFlat>)) {
    builder.StartObject(3);
    PlayerProfileFlat.AddCharacters(builder, charactersOffset);
    PlayerProfileFlat.AddItems(builder, itemsOffset);
    PlayerProfileFlat.AddGold(builder, gold);
    return PlayerProfileFlat.EndPlayerProfileFlat(builder);
  }

  public static void StartPlayerProfileFlat(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddGold(FlatBufferBuilder builder, int gold) { builder.AddInt(0, gold, 0); }
  public static void AddItems(FlatBufferBuilder builder, StringOffset itemsOffset) { builder.AddOffset(1, itemsOffset.Value, 0); }
  public static void AddCharacters(FlatBufferBuilder builder, Offset<CharacterModelFlat> charactersOffset) { builder.AddOffset(2, charactersOffset.Value, 0); }
  public static Offset<PlayerProfileFlat> EndPlayerProfileFlat(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<PlayerProfileFlat>(o);
  }
  public static void FinishPlayerProfileFlatBuffer(FlatBufferBuilder builder, Offset<PlayerProfileFlat> offset) { builder.Finish(offset.Value, "FPCS"); }
};

public struct MainCharacter : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static MainCharacter GetRootAsMainCharacter(ByteBuffer _bb) { return GetRootAsMainCharacter(_bb, new MainCharacter()); }
  public static MainCharacter GetRootAsMainCharacter(ByteBuffer _bb, MainCharacter obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public MainCharacter __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Fame { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Leadership { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<MainCharacter> CreateMainCharacter(FlatBufferBuilder builder,
      int fame = 0,
      int leadership = 0) {
    builder.StartObject(2);
    MainCharacter.AddLeadership(builder, leadership);
    MainCharacter.AddFame(builder, fame);
    return MainCharacter.EndMainCharacter(builder);
  }

  public static void StartMainCharacter(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddFame(FlatBufferBuilder builder, int fame) { builder.AddInt(0, fame, 0); }
  public static void AddLeadership(FlatBufferBuilder builder, int leadership) { builder.AddInt(1, leadership, 0); }
  public static Offset<MainCharacter> EndMainCharacter(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<MainCharacter>(o);
  }
};

public struct Party : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static Party GetRootAsParty(ByteBuffer _bb) { return GetRootAsParty(_bb, new Party()); }
  public static Party GetRootAsParty(ByteBuffer _bb, Party obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public Party __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int One { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Two { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Three { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Party> CreateParty(FlatBufferBuilder builder,
      int one = 0,
      int two = 0,
      int three = 0) {
    builder.StartObject(3);
    Party.AddThree(builder, three);
    Party.AddTwo(builder, two);
    Party.AddOne(builder, one);
    return Party.EndParty(builder);
  }

  public static void StartParty(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddOne(FlatBufferBuilder builder, int one) { builder.AddInt(0, one, 0); }
  public static void AddTwo(FlatBufferBuilder builder, int two) { builder.AddInt(1, two, 0); }
  public static void AddThree(FlatBufferBuilder builder, int three) { builder.AddInt(2, three, 0); }
  public static Offset<Party> EndParty(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<Party>(o);
  }
};

public struct CharacterModelFlat : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static CharacterModelFlat GetRootAsCharacterModelFlat(ByteBuffer _bb) { return GetRootAsCharacterModelFlat(_bb, new CharacterModelFlat()); }
  public static CharacterModelFlat GetRootAsCharacterModelFlat(ByteBuffer _bb, CharacterModelFlat obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public CharacterModelFlat __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string Name { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(4); }
  public int Level { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Exp { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<CharacterModelFlat> CreateCharacterModelFlat(FlatBufferBuilder builder,
      StringOffset nameOffset = default(StringOffset),
      int level = 0,
      int exp = 0) {
    builder.StartObject(3);
    CharacterModelFlat.AddExp(builder, exp);
    CharacterModelFlat.AddLevel(builder, level);
    CharacterModelFlat.AddName(builder, nameOffset);
    return CharacterModelFlat.EndCharacterModelFlat(builder);
  }

  public static void StartCharacterModelFlat(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(0, nameOffset.Value, 0); }
  public static void AddLevel(FlatBufferBuilder builder, int level) { builder.AddInt(1, level, 0); }
  public static void AddExp(FlatBufferBuilder builder, int exp) { builder.AddInt(2, exp, 0); }
  public static Offset<CharacterModelFlat> EndCharacterModelFlat(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<CharacterModelFlat>(o);
  }
};

