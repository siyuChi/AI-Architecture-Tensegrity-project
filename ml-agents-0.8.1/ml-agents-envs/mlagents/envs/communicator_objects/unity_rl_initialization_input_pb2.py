# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: mlagents/envs/communicator_objects/unity_rl_initialization_input.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='mlagents/envs/communicator_objects/unity_rl_initialization_input.proto',
  package='communicator_objects',
  syntax='proto3',
  serialized_options=_b('\252\002\034MLAgents.CommunicatorObjects'),
  serialized_pb=_b('\nFmlagents/envs/communicator_objects/unity_rl_initialization_input.proto\x12\x14\x63ommunicator_objects\"*\n\x1aUnityRLInitializationInput\x12\x0c\n\x04seed\x18\x01 \x01(\x05\x42\x1f\xaa\x02\x1cMLAgents.CommunicatorObjectsb\x06proto3')
)




_UNITYRLINITIALIZATIONINPUT = _descriptor.Descriptor(
  name='UnityRLInitializationInput',
  full_name='communicator_objects.UnityRLInitializationInput',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='seed', full_name='communicator_objects.UnityRLInitializationInput.seed', index=0,
      number=1, type=5, cpp_type=1, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=96,
  serialized_end=138,
)

DESCRIPTOR.message_types_by_name['UnityRLInitializationInput'] = _UNITYRLINITIALIZATIONINPUT
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

UnityRLInitializationInput = _reflection.GeneratedProtocolMessageType('UnityRLInitializationInput', (_message.Message,), dict(
  DESCRIPTOR = _UNITYRLINITIALIZATIONINPUT,
  __module__ = 'mlagents.envs.communicator_objects.unity_rl_initialization_input_pb2'
  # @@protoc_insertion_point(class_scope:communicator_objects.UnityRLInitializationInput)
  ))
_sym_db.RegisterMessage(UnityRLInitializationInput)


DESCRIPTOR._options = None
# @@protoc_insertion_point(module_scope)
