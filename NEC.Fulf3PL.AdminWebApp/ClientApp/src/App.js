import React, { Suspense } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { HeaderNav } from "./components/common/Header";
import { Footer } from "./components/common/Footer";
import { Home } from "./features/home/Home";
import { Retrigger } from "./features/retrigger/retrigger";
import { InboundTransactions } from "./features/inbound/InboundTransactions";
import { OutboundTransactions } from "./features/outbound/OutboundTransactions";
import { SKU } from "./features/skuItemMaster/SKUItemMaster";

function App() {
  return (
    <Router>
      <Suspense fallback={<div>Loading...</div>}>
        <HeaderNav />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/retrigger" element={<Retrigger />} />
          <Route path="/inboundPayload" element={<InboundTransactions />} />
          <Route path="/outboundPayload" element={<OutboundTransactions />} />
          <Route path="/SKU" element={<SKU />} />
        </Routes >
        <Footer />
      </Suspense >
    </Router >
  );
}

export default App;

