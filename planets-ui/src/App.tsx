import { QueryClient, QueryClientProvider } from "react-query";
import { HomePage, IndividualPlanetPage, Layout } from "./pages";
import { Route, Routes } from "react-router-dom";

function App() {
  const queryClient = new QueryClient({
    defaultOptions: { queries: { refetchOnWindowFocus: false } },
  });

  return (
    <QueryClientProvider client={queryClient}>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<HomePage />} />
          <Route path=":planetId" element={<IndividualPlanetPage />} />
        </Route>
      </Routes>
    </QueryClientProvider>
  );
}

export default App;
