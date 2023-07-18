import { QueryClient, QueryClientProvider } from "react-query";
import { HomePage, IndividualPlanetPage, Layout } from "./pages";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { homePageDataLoader } from "./pages/Home/homePageDataLoader";
import { individualPlanetPageDataLoader } from "./pages/IndividualPlanet/individualPlanetPageDataLoader";

function App() {
  const queryClient = new QueryClient({
    defaultOptions: { queries: { refetchOnWindowFocus: false } },
  });

  const router = createBrowserRouter([
    {
      path: "/",
      Component: Layout,
      children: [
        {
          index: true,
          loader: homePageDataLoader,
          Component: HomePage,
        },
        {
          path: ":planetId",
          loader: individualPlanetPageDataLoader,
          Component: IndividualPlanetPage,
        },
      ],
    },
  ]);

  return (
    <QueryClientProvider client={queryClient}>
      <RouterProvider router={router} />
    </QueryClientProvider>
  );
}

export default App;
