﻿using System;
using System.Threading.Tasks;

namespace NServiceBus.AmbientSession
{
    public static class BusSessionExtensions
    {
        /// <summary>
        /// Sends the provided message.
        /// </summary>
        /// <param name="session">The instance of <see cref="IMessageSession" /> to use for the action.</param>
        /// <param name="message">The message to send.</param>
        public static Task Send(this IMessageSession session, object message)
        {
            Guard.AgainstNull(nameof(session), session);
            Guard.AgainstNull(nameof(message), message);

            return session.Send(message, new SendOptions());
        }

        /// <summary>
        /// Instantiates a message of <typeparamref name="T" /> and sends it.
        /// </summary>
        /// <typeparam name="T">The type of message, usually an interface.</typeparam>
        /// <param name="session">The instance of <see cref="IMessageSession" /> to use for the action.</param>
        /// <param name="messageConstructor">An action which initializes properties of the message.</param>
        /// <remarks>
        /// The message will be sent to the destination configured for <typeparamref name="T" />.
        /// </remarks>
        public static Task Send<T>(this IMessageSession session, Action<T> messageConstructor)
        {
            Guard.AgainstNull(nameof(session), session);
            Guard.AgainstNull(nameof(messageConstructor), messageConstructor);

            return session.Send(messageConstructor, new SendOptions());
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="session">The instance of <see cref="IMessageSession" /> to use for the action.</param>
        /// <param name="destination">The address of the destination to which the message will be sent.</param>
        /// <param name="message">The message to send.</param>
        public static Task Send(this IMessageSession session, string destination, object message)
        {
            Guard.AgainstNull(nameof(session), session);
            Guard.AgainstNullAndEmpty(nameof(destination), destination);
            Guard.AgainstNull(nameof(message), message);

            var options = new SendOptions();

            options.SetDestination(destination);

            return session.Send(message, options);
        }

        /// <summary>
        /// Instantiates a message of type T and sends it to the given destination.
        /// </summary>
        /// <typeparam name="T">The type of message, usually an interface.</typeparam>
        /// <param name="session">The instance of <see cref="IMessageSession" /> to use for the action.</param>
        /// <param name="destination">The destination to which the message will be sent.</param>
        /// <param name="messageConstructor">An action which initializes properties of the message.</param>
        public static Task Send<T>(this IMessageSession session, string destination, Action<T> messageConstructor)
        {
            Guard.AgainstNull(nameof(session), session);
            Guard.AgainstNullAndEmpty(nameof(destination), destination);
            Guard.AgainstNull(nameof(messageConstructor), messageConstructor);

            var options = new SendOptions();

            options.SetDestination(destination);

            return session.Send(messageConstructor, options);
        }

        /// <summary>
        /// Sends the message back to the current endpoint.
        /// </summary>
        /// <param name="session">Object being extended.</param>
        /// <param name="message">The message to send.</param>
        public static Task SendLocal(this IMessageSession session, object message)
        {
            Guard.AgainstNull(nameof(session), session);
            Guard.AgainstNull(nameof(message), message);

            var options = new SendOptions();

            options.RouteToThisEndpoint();

            return session.Send(message, options);
        }

        /// <summary>
        /// Instantiates a message of type T and sends it back to the current endpoint.
        /// </summary>
        /// <typeparam name="T">The type of message, usually an interface.</typeparam>
        /// <param name="session">Object being extended.</param>
        /// <param name="messageConstructor">An action which initializes properties of the message.</param>
        public static Task SendLocal<T>(this IMessageSession session, Action<T> messageConstructor)
        {
            Guard.AgainstNull(nameof(session), session);
            Guard.AgainstNull(nameof(messageConstructor), messageConstructor);

            var options = new SendOptions();

            options.RouteToThisEndpoint();

            return session.Send(messageConstructor, options);
        }

        /// <summary>
        /// Publish the message to subscribers.
        /// </summary>
        /// <param name="session">The instance of <see cref="IMessageSession" /> to use for the action.</param>
        /// <param name="message">The message to publish.</param>
        public static Task Publish(this IMessageSession session, object message)
        {
            Guard.AgainstNull(nameof(session), session);
            Guard.AgainstNull(nameof(message), message);

            return session.Publish(message, new PublishOptions());
        }

        /// <summary>
        /// Publish the message to subscribers.
        /// </summary>
        /// <param name="session">The instance of <see cref="IMessageSession" /> to use for the action.</param>
        /// <typeparam name="T">The message type.</typeparam>
        public static Task Publish<T>(this IMessageSession session)
        {
            Guard.AgainstNull(nameof(session), session);

            return session.Publish<T>(_ => { }, new PublishOptions());
        }

        /// <summary>
        /// Instantiates a message of type T and publishes it.
        /// </summary>
        /// <typeparam name="T">The type of message, usually an interface.</typeparam>
        /// <param name="session">The instance of <see cref="IMessageSession" /> to use for the action.</param>
        /// <param name="messageConstructor">An action which initializes properties of the message.</param>
        public static Task Publish<T>(this IMessageSession session, Action<T> messageConstructor)
        {
            Guard.AgainstNull(nameof(session), session);
            Guard.AgainstNull(nameof(messageConstructor), messageConstructor);

            return session.Publish(messageConstructor, new PublishOptions());
        }
    }
}