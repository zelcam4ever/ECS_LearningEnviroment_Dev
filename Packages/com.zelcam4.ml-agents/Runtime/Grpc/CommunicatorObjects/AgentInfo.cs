// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: mlagents_envs/communicator_objects/agent_info.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Zelcam4.MLAgents.CommunicatorObjects {

  /// <summary>Holder for reflection information generated from mlagents_envs/communicator_objects/agent_info.proto</summary>
  internal static partial class AgentInfoReflection {

    #region Descriptor
    /// <summary>File descriptor for mlagents_envs/communicator_objects/agent_info.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static AgentInfoReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CjNtbGFnZW50c19lbnZzL2NvbW11bmljYXRvcl9vYmplY3RzL2FnZW50X2lu",
            "Zm8ucHJvdG8SFGNvbW11bmljYXRvcl9vYmplY3RzGjRtbGFnZW50c19lbnZz",
            "L2NvbW11bmljYXRvcl9vYmplY3RzL29ic2VydmF0aW9uLnByb3RvIvkBCg5B",
            "Z2VudEluZm9Qcm90bxIOCgZyZXdhcmQYByABKAISDAoEZG9uZRgIIAEoCBIY",
            "ChBtYXhfc3RlcF9yZWFjaGVkGAkgASgIEgoKAmlkGAogASgFEhMKC2FjdGlv",
            "bl9tYXNrGAsgAygIEjwKDG9ic2VydmF0aW9ucxgNIAMoCzImLmNvbW11bmlj",
            "YXRvcl9vYmplY3RzLk9ic2VydmF0aW9uUHJvdG8SEAoIZ3JvdXBfaWQYDiAB",
            "KAUSFAoMZ3JvdXBfcmV3YXJkGA8gASgCSgQIARACSgQIAhADSgQIAxAESgQI",
            "BBAFSgQIBRAGSgQIBhAHSgQIDBANQiWqAiJVbml0eS5NTEFnZW50cy5Db21t",
            "dW5pY2F0b3JPYmplY3RzYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Zelcam4.MLAgents.CommunicatorObjects.ObservationReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Zelcam4.MLAgents.CommunicatorObjects.AgentInfoProto), global::Zelcam4.MLAgents.CommunicatorObjects.AgentInfoProto.Parser, new[]{ "Reward", "Done", "MaxStepReached", "Id", "ActionMask", "Observations", "GroupId", "GroupReward" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  internal sealed partial class AgentInfoProto : pb::IMessage<AgentInfoProto> {
    private static readonly pb::MessageParser<AgentInfoProto> _parser = new pb::MessageParser<AgentInfoProto>(() => new AgentInfoProto());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<AgentInfoProto> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Zelcam4.MLAgents.CommunicatorObjects.AgentInfoReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AgentInfoProto() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AgentInfoProto(AgentInfoProto other) : this() {
      reward_ = other.reward_;
      done_ = other.done_;
      maxStepReached_ = other.maxStepReached_;
      id_ = other.id_;
      actionMask_ = other.actionMask_.Clone();
      observations_ = other.observations_.Clone();
      groupId_ = other.groupId_;
      groupReward_ = other.groupReward_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AgentInfoProto Clone() {
      return new AgentInfoProto(this);
    }

    /// <summary>Field number for the "reward" field.</summary>
    public const int RewardFieldNumber = 7;
    private float reward_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Reward {
      get { return reward_; }
      set {
        reward_ = value;
      }
    }

    /// <summary>Field number for the "done" field.</summary>
    public const int DoneFieldNumber = 8;
    private bool done_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Done {
      get { return done_; }
      set {
        done_ = value;
      }
    }

    /// <summary>Field number for the "max_step_reached" field.</summary>
    public const int MaxStepReachedFieldNumber = 9;
    private bool maxStepReached_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool MaxStepReached {
      get { return maxStepReached_; }
      set {
        maxStepReached_ = value;
      }
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 10;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "action_mask" field.</summary>
    public const int ActionMaskFieldNumber = 11;
    private static readonly pb::FieldCodec<bool> _repeated_actionMask_codec
        = pb::FieldCodec.ForBool(90);
    private readonly pbc::RepeatedField<bool> actionMask_ = new pbc::RepeatedField<bool>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<bool> ActionMask {
      get { return actionMask_; }
    }

    /// <summary>Field number for the "observations" field.</summary>
    public const int ObservationsFieldNumber = 13;
    private static readonly pb::FieldCodec<global::Zelcam4.MLAgents.CommunicatorObjects.ObservationProto> _repeated_observations_codec
        = pb::FieldCodec.ForMessage(106, global::Zelcam4.MLAgents.CommunicatorObjects.ObservationProto.Parser);
    private readonly pbc::RepeatedField<global::Zelcam4.MLAgents.CommunicatorObjects.ObservationProto> observations_ = new pbc::RepeatedField<global::Zelcam4.MLAgents.CommunicatorObjects.ObservationProto>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Zelcam4.MLAgents.CommunicatorObjects.ObservationProto> Observations {
      get { return observations_; }
    }

    /// <summary>Field number for the "group_id" field.</summary>
    public const int GroupIdFieldNumber = 14;
    private int groupId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int GroupId {
      get { return groupId_; }
      set {
        groupId_ = value;
      }
    }

    /// <summary>Field number for the "group_reward" field.</summary>
    public const int GroupRewardFieldNumber = 15;
    private float groupReward_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float GroupReward {
      get { return groupReward_; }
      set {
        groupReward_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as AgentInfoProto);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(AgentInfoProto other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Reward, other.Reward)) return false;
      if (Done != other.Done) return false;
      if (MaxStepReached != other.MaxStepReached) return false;
      if (Id != other.Id) return false;
      if(!actionMask_.Equals(other.actionMask_)) return false;
      if(!observations_.Equals(other.observations_)) return false;
      if (GroupId != other.GroupId) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(GroupReward, other.GroupReward)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Reward != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Reward);
      if (Done != false) hash ^= Done.GetHashCode();
      if (MaxStepReached != false) hash ^= MaxStepReached.GetHashCode();
      if (Id != 0) hash ^= Id.GetHashCode();
      hash ^= actionMask_.GetHashCode();
      hash ^= observations_.GetHashCode();
      if (GroupId != 0) hash ^= GroupId.GetHashCode();
      if (GroupReward != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(GroupReward);
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Reward != 0F) {
        output.WriteRawTag(61);
        output.WriteFloat(Reward);
      }
      if (Done != false) {
        output.WriteRawTag(64);
        output.WriteBool(Done);
      }
      if (MaxStepReached != false) {
        output.WriteRawTag(72);
        output.WriteBool(MaxStepReached);
      }
      if (Id != 0) {
        output.WriteRawTag(80);
        output.WriteInt32(Id);
      }
      actionMask_.WriteTo(output, _repeated_actionMask_codec);
      observations_.WriteTo(output, _repeated_observations_codec);
      if (GroupId != 0) {
        output.WriteRawTag(112);
        output.WriteInt32(GroupId);
      }
      if (GroupReward != 0F) {
        output.WriteRawTag(125);
        output.WriteFloat(GroupReward);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Reward != 0F) {
        size += 1 + 4;
      }
      if (Done != false) {
        size += 1 + 1;
      }
      if (MaxStepReached != false) {
        size += 1 + 1;
      }
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      size += actionMask_.CalculateSize(_repeated_actionMask_codec);
      size += observations_.CalculateSize(_repeated_observations_codec);
      if (GroupId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(GroupId);
      }
      if (GroupReward != 0F) {
        size += 1 + 4;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(AgentInfoProto other) {
      if (other == null) {
        return;
      }
      if (other.Reward != 0F) {
        Reward = other.Reward;
      }
      if (other.Done != false) {
        Done = other.Done;
      }
      if (other.MaxStepReached != false) {
        MaxStepReached = other.MaxStepReached;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      actionMask_.Add(other.actionMask_);
      observations_.Add(other.observations_);
      if (other.GroupId != 0) {
        GroupId = other.GroupId;
      }
      if (other.GroupReward != 0F) {
        GroupReward = other.GroupReward;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 61: {
            Reward = input.ReadFloat();
            break;
          }
          case 64: {
            Done = input.ReadBool();
            break;
          }
          case 72: {
            MaxStepReached = input.ReadBool();
            break;
          }
          case 80: {
            Id = input.ReadInt32();
            break;
          }
          case 90:
          case 88: {
            actionMask_.AddEntriesFrom(input, _repeated_actionMask_codec);
            break;
          }
          case 106: {
            observations_.AddEntriesFrom(input, _repeated_observations_codec);
            break;
          }
          case 112: {
            GroupId = input.ReadInt32();
            break;
          }
          case 125: {
            GroupReward = input.ReadFloat();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
