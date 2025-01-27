﻿using System;

namespace MadXchange.Common
{
    /// <summary>
    /// Represents the return value of a method that was executed remotely.
    /// </summary>
    public class InvocationResult
    {
        /// <summary>
        /// Gets or sets the unique identifier associated with the invocation.
        /// </summary>
        /// <value>The unique identifier of the invocation.</value>
        
        public Guid Identifier { get; set; }

        /// <summary>
        /// Gets or sets the result of the method call.
        /// </summary>
        /// <value>The result of the method call.</value>
       
        public object Result { get; set; }

        /// <summary>
        /// Gets or sets the remote exception the method call caused.
        /// </summary>
        /// <value>The remote exception of the method call.</value>
        
        public Exception Exception { get; set; }
    }
    
}