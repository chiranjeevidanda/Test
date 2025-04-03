import React from "react";
import { NavLink } from "react-router-dom";

export function HeaderNav() {
  //   state = { activeItem: 'home' }

  //   handleItemClick = (e, { name }) => this.setState({ activeItem: name });
  // const { activeItem } = this.state

  return (
    <header className="bg-aa8453 text-white">
      <div className="container mx-auto px-6 py-5 flex justify-between items-center">
        <div className="text-2xl font-bold text-gray-900">Katoen Admin</div>
        <nav className="flex space-x-4">
          <NavLink to="/" className="text-gray-700 hover:text-gray-900 hover:underline ">Dashboard</NavLink>
          {/*<NavLink to="/financialdocument" className="text-gray-700 hover:text-gray-900 hover:underline">Financial Documents</NavLink>*/}
          {/*<NavLink to="/retrigger" className="text-gray-700 hover:text-gray-900 hover:underline">Retrigger</NavLink>          */}
          {/*<NavLink to="/faileddocument" className="text-gray-700 hover:text-gray-900 hover:underline">Failed Documents</NavLink>*/}

          <NavLink to="/retrigger" className="text-gray-700 hover:text-gray-900 hover:underline">Retrigger</NavLink>
          <NavLink to="/inboundpayload" className="text-gray-700 hover:text-gray-900 hover:underline">View Inbound Payloads</NavLink>
          <NavLink to="/outboundpayload" className="text-gray-700 hover:text-gray-900 hover:underline">View Outbound Payloads</NavLink>
          <NavLink to="/sku" className="text-gray-700 hover:text-gray-900 hover:underline">SKU</NavLink>
        </nav>
      </div>
    </header>
  );
}
