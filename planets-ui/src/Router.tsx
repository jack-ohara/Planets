import { useQuery } from "react-query";

export function Router() {
  const query = useQuery(
    "planets",
    async () => {
      const response = await fetch(
        `${import.meta.env.VITE_API_URL as string}/Planets`
      );
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    },
    { retry: false, cacheTime: 0 }
  );

  return query.isLoading ? (
    <div>Loading...</div>
  ) : (
    <code>{JSON.stringify(query.data)}</code>
  );
}
