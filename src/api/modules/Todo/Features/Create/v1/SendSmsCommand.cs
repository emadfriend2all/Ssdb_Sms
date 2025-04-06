﻿using System.ComponentModel;
using MediatR;

namespace FSH.Starter.WebApi.Todo.Features.Create.v1;
public record SendSmsCommand(
    string phoneNumber,
    string message) : IRequest<SendSmsResponse>;



